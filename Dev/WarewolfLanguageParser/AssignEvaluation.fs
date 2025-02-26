﻿module AssignEvaluation
open Microsoft.FSharp.Text.Lexing
open DataASTMutable
open WarewolfDataEvaluationCommon
open Dev2.Common.Interfaces
open LanguageAST




let CreateDataSet (a:string) =
    let col = new WarewolfParserInterop.WarewolfAtomList<WarewolfAtomRecord>(WarewolfAtomRecord.Nothing)
    {
        Data = [(PositionColumn,col) ] |> Map.ofList
        Optimisations = Ordinal;
        LastIndex=0;

        Frame=0;
    }

let AddRecsetToEnv (name:string) (env:WarewolfEnvironment) = 
    if env.RecordSets.ContainsKey name
    then
       env
    else
       let b = CreateDataSet ""
       let a = {env with RecordSets= (Map.add name b env.RecordSets);}
       a

let AddToScalars (env:WarewolfEnvironment) (name:string) (value:WarewolfAtom)  =
    let rem = Map.remove name env.Scalar |> Map.add name value 
    {       Scalar=rem;
            RecordSets = env.RecordSets
    }

let rec AddToRecordSet (env:WarewolfEnvironment) (name:RecordSetIdentifier) (update:int) (value:WarewolfAtom)  =
    if(env.RecordSets.ContainsKey name.Name)
    then
        let recordset = env.RecordSets.[name.Name]
        let recsetAdded = match name.Index with
                          | IntIndex a -> AddAtomToRecordSet recordset name.Column value a
                          | Star -> UpdateColumnWithValue recordset name.Column value 
                          | Last -> AddAtomToRecordSet recordset name.Column value (recordset.LastIndex+1)
                          | IndexExpression a -> AddAtomToRecordSet recordset name.Column value (EvalIndex env update ( LanguageExpressionToString a ))
        let recsets = Map.remove name.Name env.RecordSets |> fun a-> Map.add name.Name recsetAdded a
        { env with RecordSets = recsets}
    else
        let envwithRecset = AddRecsetToEnv name.Name env
        AddToRecordSet envwithRecset name update value
    
and EvalAssign (exp:string) (value:string) (update:int) (env:WarewolfEnvironment) =
    EvalAssignWithFrame (new WarewolfParserInterop.AssignValue(exp,value)) update env
    

and AddToRecordSetFramed (env:WarewolfEnvironment) (name:RecordSetIdentifier) (value:WarewolfAtom)  =
    if(env.RecordSets.ContainsKey name.Name)
    then
        let recordset = env.RecordSets.[name.Name]
        let recsetAdded = match name.Index with
                          | IntIndex a -> AddAtomToRecordSetWithFraming recordset name.Column value a false
                          | Star ->  if recordset.Count =0 then
                                        AddAtomToRecordSetWithFraming recordset name.Column value 1 false
                                     else
                                        UpdateColumnWithValue recordset name.Column value 
                          | Last -> AddAtomToRecordSetWithFraming recordset name.Column value (getPositionFromRecset recordset  name.Column) true
                          | IndexExpression a -> AddAtomToRecordSetWithFraming recordset name.Column value (EvalIndex env 0 ( LanguageExpressionToString a )) false
        let recsets = Map.remove name.Name env.RecordSets |> fun a-> Map.add name.Name recsetAdded a
        { env with RecordSets = recsets}
    else
        let envwithRecset = AddRecsetToEnv name.Name env
        AddToRecordSetFramed envwithRecset name value

and  AddToRecordSetFramedWithAtomList (env:WarewolfEnvironment) (name:RecordSetIdentifier) (value:WarewolfAtom seq) (shouldUseLast:bool) (update:int)  (assignValue :   IAssignValue option  )  =
    
    if(env.RecordSets.ContainsKey name.Name)
    then
        let data = env.RecordSets.[name.Name]
        let recordset =  if data.Data.ContainsKey( name.Column) 
                         then  data
                         else 
                            { data with Data=  Map.add name.Column (CreateEmpty data.Data.[PositionColumn].Length data.Data.[PositionColumn].Count )  data.Data    }
        let recsetAdded = match name.Index with
                          | IntIndex a -> AddAtomToRecordSetWithFraming recordset name.Column (Seq.last value) a false
                          | Star -> 
                                    let countVals = Seq.length value 
                                    let mutable recsetmutated = recordset
                                    let mutable index = 1 
                                    match  shouldUseLast with  
                                           | false->    for a in value do  
                                                            recsetmutated<-AddAtomToRecordSetWithFraming recsetmutated name.Column a index false  
                                                            index<-index+1
                                                        recsetmutated
                                           | true ->        
                                                       let col = recsetmutated.Data.[name.Column]
                                                       let valueToChange = Seq.last value
                                                       for a in [0..col.Count-1]  do  
                                                            recsetmutated<-AddAtomToRecordSetWithFraming recsetmutated name.Column valueToChange (a+1) false  
                                                            index<-index+1
                                                       recsetmutated                                   
                                        
                          | Last -> 
                                    let countVals = Seq.length value 
                                    let mutable recsetmutated = recordset
                                    let mutable index = recordset.LastIndex+1   
                                    for a in value do  
                                        recsetmutated<-AddAtomToRecordSetWithFraming recordset name.Column a index false  
                                        index<-index+1
                                    recsetmutated   
                          | IndexExpression b -> 
                                   let res = Eval env update (LanguageExpressionToString b ) |> EvalResultToString
                                   match b,assignValue with 
                                        | WarewolfAtomAtomExpression atom,_ ->
                                                    match atom with
                                                    | Int a ->  AddAtomToRecordSetWithFraming recordset name.Column (Seq.last value) a false
                                                    | _ -> failwith "Invalid index"
                                        | _, Some av  ->   let data = (EvalAssignWithFrame  (new WarewolfParserInterop.AssignValue( (sprintf "[[%s(%s).%s]]" name.Name res name.Column), av.Value)) update env) :WarewolfEnvironment
                                                           data.RecordSets.[name.Name] 
                                        |_,_ ->  failwith "Invalid assign from list"
        let recsets = Map.remove name.Name env.RecordSets |> fun a-> Map.add name.Name recsetAdded a
        { env with RecordSets = recsets}
    else
        let envwithRecset = AddRecsetToEnv name.Name env
        AddToRecordSetFramedWithAtomList envwithRecset name value shouldUseLast update assignValue

and EvalMultiAssignOp  (env:WarewolfEnvironment) (update:int)  (value :IAssignValue ) =
    let l = WarewolfDataEvaluationCommon.ParseLanguageExpression value.Name update
    let left = match l with 
                    |ComplexExpression a -> if List.exists (fun a -> match a with
                                                                            | ScalarExpression a -> true
                                                                            | RecordSetExpression a -> true
                                                                            | _->false) a then    l  else LanguageExpression.WarewolfAtomAtomExpression  (LanguageExpressionToString l|> DataString )                         
                    | _-> l
    let rightParse = if value.Value=null then LanguageExpression.WarewolfAtomAtomExpression Nothing
                     else WarewolfDataEvaluationCommon.ParseLanguageExpression value.Value  update
    
    let right = if value.Value=null then WarewolfAtomResult Nothing
                else WarewolfDataEvaluationCommon.Eval env update value.Value 
    let shouldUseLast =  match rightParse with
                            | RecordSetExpression a ->
                                        match a.Index with
                                            | IntIndex a-> true
                                            | Star -> false
                                            | Last -> true
                                            | _-> true
                            | _->true                  
    match right with 
                | WarewolfAtomResult x -> 
                            match left with 
                            |   ScalarExpression a -> AddToScalars env a x
                            |   RecordSetExpression b -> AddToRecordSetFramed env b x
                            |   WarewolfAtomAtomExpression a -> failwith "invalid variabe assigned to"
                            |   _ -> let expression = (EvalToExpression env update value.Name)
                                     if System.String.IsNullOrEmpty(  expression) || ( expression) = "[[]]" || ( expression) = value.Name then
                                        env
                                     else
                                        EvalMultiAssignOp env update (new WarewolfParserInterop.AssignValue(  expression , value.Value))
                | WarewolfAtomListresult x -> 
                        match left with 
                        |   ScalarExpression a -> AddToScalars env a (Seq.last x)
                        |   RecordSetExpression b -> AddToRecordSetFramedWithAtomList env b  x shouldUseLast update (Some value)
                        |   WarewolfAtomAtomExpression a ->  failwith "invalid variabe assigned to"
                        |    _ -> let expression = (EvalToExpression env update value.Name)
                                  if System.String.IsNullOrEmpty(  expression) || ( expression) = "[[]]" || ( expression) = value.Name then
                                        env
                                  else
                                        EvalMultiAssignOp env  update (new WarewolfParserInterop.AssignValue(  expression , value.Value))
                |   _ -> failwith "assigning an entire recordset to a variable is not defined"

and EvalMultiAssignList (env:WarewolfEnvironment)  (value :WarewolfAtom seq ) (exp :string) (update:int) (shouldUseLast: bool)=
    let left = WarewolfDataEvaluationCommon.ParseLanguageExpression exp update
    match left with 
        |   RecordSetExpression b -> AddToRecordSetFramedWithAtomList env b  value shouldUseLast update None
        |    _ ->   failwith "only recsets"


and EvalDataShape (exp:string) (update:int) (env:WarewolfEnvironment) =
    let left = WarewolfDataEvaluationCommon.ParseLanguageExpression exp update
    match left with 
    |   ScalarExpression a -> match env.Scalar.TryFind a with
                              | None -> AddToScalars env a Nothing
                              | Some x -> AddToScalars env a Nothing
    |   RecordSetExpression name -> match env.RecordSets.TryFind name.Name with
                                      | None -> let envwithRecset = AddRecsetToEnv name.Name env
                                                let data = envwithRecset.RecordSets.[name.Name]
                                                let recordset =  if data.Data.ContainsKey( name.Column) 
                                                                 then  data
                                                                 else 
                                                                    { data with Data=  Map.add name.Column (CreateEmpty data.Data.[PositionColumn].Length data.Data.[PositionColumn].Count )  data.Data    }
                                                ReplaceDataset env recordset name.Name
                                        
                                      | Some data -> let recordset =  if data.Data.ContainsKey( name.Column)  then  
                                                                         data
                                                                      else 
                                                                          { data with Data=  Map.add name.Column (CreateEmpty data.Data.[PositionColumn].Length data.Data.[PositionColumn].Count )  data.Data    }
                                                     ReplaceDataset env recordset name.Name

    |   WarewolfAtomAtomExpression a -> env
    |   _ -> failwith "input must be recordset or value"

and ReplaceDataset (env:WarewolfEnvironment)  (data:WarewolfRecordset) (name:string)=
    let recsets = Map.remove name env.RecordSets |> fun a-> Map.add name data a
    { env with RecordSets = recsets} 
and EvalMultiAssign (values :IAssignValue seq) (update:int) (env:WarewolfEnvironment) =
        let env = Seq.fold (fun a b->  EvalMultiAssignOp a update b)  env  values
        let recsets = Map.map (fun a b -> {b with Frame = 0 }) env.RecordSets
        {env with RecordSets = recsets}

and  UpdateColumnWithValue (rset:WarewolfRecordset) (columnName:string) (value: WarewolfAtom)=
        if rset.Data.ContainsKey( columnName) 
        then
            let x = rset.Data.[columnName];
            for i in [0..x.Count-1] do                         
                x.[i]<-value;    
            rset 
        else 
        {rset with Data=  Map.add columnName ( CreateFilled rset.Count value)  rset.Data    }



and EvalAssignWithFrame (value :IAssignValue ) (update:int) (env:WarewolfEnvironment) =
        let envass = EvalMultiAssignOp env update value
        let recsets = envass.RecordSets
        {envass with RecordSets = recsets}

let RemoveFraming  (env:WarewolfEnvironment) =
        let recsets = Map.map (fun a b -> {b with Frame = 0 }) env.RecordSets
        {env with RecordSets = recsets}

let AtomtoString a = WarewolfDataEvaluationCommon.AtomtoString a;
