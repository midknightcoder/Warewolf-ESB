
/*
*  Warewolf - The Easy Service Bus
*  Copyright 2015 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later. 
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2;
using Dev2.Data.Enums;
using Dev2.Interfaces;
using Dev2.Studio.Core.Helpers;
using Dev2.TO;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
namespace Unlimited.Applications.BusinessDesignStudio.Activities
// ReSharper restore CheckNamespace
{
    public class DTOFactory
    {

        public static IDev2TOFn CreateNewDTO(IDev2TOFn dto, int index = 0, bool inserted = false, string initializeWith = "")
        {
            IDev2TOFn toReturn = null;

            TypeSwitch.Do(dto,
                // ReSharper disable ImplicitlyCapturedClosure
                TypeSwitch.Case<ActivityDTO>(x => toReturn = new ActivityDTO(initializeWith, "", index, inserted)),
                // ReSharper restore ImplicitlyCapturedClosure
                TypeSwitch.Case<DataSplitDTO>(x =>
                {
                    var dataSplitDto = dto as DataSplitDTO;
                    if(dataSplitDto != null)
                    {
                        toReturn = new DataSplitDTO(initializeWith, dataSplitDto.SplitType, dataSplitDto.At, index, false, inserted);
                    }
                }),
                TypeSwitch.Case<DataMergeDTO>(x =>
                {
                    var dataMergeDto = dto as DataMergeDTO;
                    if(dataMergeDto != null)
                    {
                        toReturn = new DataMergeDTO(initializeWith, dataMergeDto.MergeType, dataMergeDto.At, index, dataMergeDto.Padding, dataMergeDto.Alignment, inserted);
                    }
                }),
                TypeSwitch.Case<CaseConvertTO>(x =>
                {
                    var caseConvertTO = dto as CaseConvertTO;
                    if(caseConvertTO != null)
                    {
                        toReturn = CaseConverterFactory.CreateCaseConverterTO(initializeWith, caseConvertTO.ConvertType, caseConvertTO.Result, index);
                    }
                }),
                TypeSwitch.Case<BaseConvertTO>(x =>
                {
                    var baseConvertTO = dto as BaseConvertTO;
                    if(baseConvertTO != null)
                    {
                        toReturn = new BaseConvertTO(initializeWith, baseConvertTO.FromType, baseConvertTO.ToType, baseConvertTO.ToExpression, index, inserted);
                    }
                }),
                // ReSharper disable ImplicitlyCapturedClosure
                TypeSwitch.Case<GatherSystemInformationTO>(x => toReturn =
                    new GatherSystemInformationTO(enTypeOfSystemInformationToGather.FullDateTime,
                        initializeWith, index, inserted)),
                TypeSwitch.Case<XPathDTO>(x => toReturn = new XPathDTO(initializeWith, "", index, inserted)),
                TypeSwitch.Case<FindRecordsTO>(() => toReturn = new FindRecordsTO("", "", index, inserted)),
                TypeSwitch.Case<JsonMappingTo>(() => toReturn = new JsonMappingTo(initializeWith, index, inserted)),
                TypeSwitch.Case<SharepointSearchTo>(() => toReturn = new SharepointSearchTo(initializeWith, "=", "", index, inserted)),
                TypeSwitch.Case<SharepointReadListTo>(() => toReturn = new SharepointReadListTo("", initializeWith, "","")),
                //REPLACE WITH SHAREPOINT DELETE ACTIVITY
                //TypeSwitch.Case<SharepointReadListTo>(() => toReturn = new SharepointReadListTo("", initializeWith, "")),
            TypeSwitch.Default(() => toReturn = null));

            return toReturn;
        }
    }
}
