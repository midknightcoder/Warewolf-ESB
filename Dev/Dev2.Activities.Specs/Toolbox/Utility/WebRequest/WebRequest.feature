﻿Feature: WebRequest
	In order to download html content
	As a Warewolf user
	I want a tool that I can input a url and get a html document


Scenario: Enter a URL to download html  
	Given I have the url "http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx?html"	
	When the web request tool is executed 
	Then the result should contain the string "Welcome to ASP.NET Web API"
	And the execution has "NO" error
	And the debug inputs as  
	| URL                                                        | Header |
	| http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx?html |        |
	And the debug output as 
	|                     |
	| [[result]] = String |
	
Scenario Outline: Enter a URL to download html with timeout specified 
	Given I have the url '<url>' with timeoutSeconds '<timeoutSeconds>'
	When the web request tool is executed 
	Then the result should contain the string "<result>"
	And the execution has "NO" error
	And the debug inputs as  
	| URL   | Header | Time Out Seconds |
	| <url> |        | <timeoutSeconds> |
	And the debug output as 
	|                     |
	| [[result]] = String |
	Examples:
	| url                                                   | timeoutSeconds | result          |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=15  | 20             | Wait Successful |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=110 | 120            | Wait Successful |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=110 | 0              | Wait Successful |
	
Scenario Outline: Enter a URL to download html with timeout specified too short 
	Given I have the url '<url>' with timeoutSeconds '<timeoutSeconds>'
	When the web request tool is executed 
	Then the execution has "AN" error
	And the debug inputs as  
	| URL   | Header | Time Out Seconds |
	| <url> |        | <timeoutSeconds> |
	And the debug output as 
	|                     |
	| [[result]] = String |
	Examples:
	| url                                                   | timeoutSeconds |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=150 | 10             |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=15  | 10             |

Scenario: Enter a badly formed URL
	Given I have the url "www.google.comx"	
	When the web request tool is executed 
	Then the result should contain the string ""
	And the execution has "AN" error
	And the debug inputs as  
	| URL             | Header |
	| www.google.comx |        |
	And the debug output as 
	|              |
	| [[result]] = |

Scenario: Enter a URL made up of text and variables with no header
    Given I have the url "http://[[site]][[file]]"	
	And I have a web request variable "[[site]]" equal to "rsaklfsvrtfsbld/IntegrationTestSite/"	
	And I have a web request variable "[[file]]" equal to "Proxy.ashx?html"
	When the web request tool is executed 
	Then the result should contain the string "Welcome to ASP.NET Web API"
	And the execution has "NO" error
	And the debug inputs as  
	| URL                                                                                  | Header |
	| http://[[site]][[file]] = http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx?html |        |
	And the debug output as 
	|                     |
	| [[result]] = String |


Scenario: Enter a URL and 2 variables each with a header parameter (json)
	Given I have the url "http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx"	
	And I have a web request variable "[[ContentType]]" equal to "Content-Type"	
	And I have a web request variable "[[Type]]" equal to "application/json"	
	And I have the Header "[[ContentType]]: [[Type]]"
	When the web request tool is executed 
	Then the result should contain the string "["value1","value2"]"
	And the execution has "NO" error
	And the debug inputs as  
	| URL                                                   | Header                                                      |
	| http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx | [[ContentType]]: [[Type]] = Content-Type: application/json" |
	And the debug output as 
	|                                  |
	| [[result]] = ["value1","value2"] |

Scenario: Enter a URL and 2 variables each with a header parameter (xml)
	Given I have the url "http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx"	
	And I have a web request variable "[[ContentType]]" equal to "Content-Type"	
	And I have a web request variable "[[Type]]" equal to "application/xml"	
	And I have the Header "[[ContentType]]: [[Type]]"
	When the web request tool is executed 
	Then the result should contain the string "<string>value1</string>"
	And the execution has "NO" error
	And the debug inputs as  
	| URL                                                   | Header                                                     |
	| http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx | [[ContentType]]: [[Type]] = Content-Type: application/xml" |
	And the debug output as 
	|                                      |
	| [[result]] = <string>value1</string> |

Scenario: Enter a URL that returns json
	Given I have the url "http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx?json"	
	When the web request tool is executed	
	Then the result should contain the string "["value1","value2"]"
	And the execution has "NO" error
	And the debug inputs as  
	| URL                                                        | Header |
	| http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx?json |        |
	And the debug output as 
	|                                  |
	| [[result]] = ["value1","value2"] |

Scenario: Enter a URL that returns xml
	Given I have the url "http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx?xml"
	When the web request tool is executed	
	Then the result should contain the string "<string>value1</string>"
	And the execution has "NO" error
	And the debug inputs as  
	| URL                                                       | Header |
	| http://rsaklfsvrtfsbld/IntegrationTestSite/Proxy.ashx?xml |        |
	And the debug output as 
	|                                      |
	| [[result]] = <string>value1</string> |

Scenario: Enter a blank URL
	Given I have the url ""
	When the web request tool is executed	
	Then the result should contain the string ""
	And the execution has "AN" error
	And the debug inputs as  
	| URL | Header |
	| ""  |        |
	And the debug output as 
	|              |
	| [[result]] = |

Scenario: Enter a URL that is a negative index recordset
	Given I have the url "[[rec(-1).set]]"
	When the web request tool is executed	
	Then the result should contain the string ""
	And the execution has "AN" error
	And the debug inputs as  
	| URL               | Header |
	| [[rec(-1).set]] = |        |
	And the debug output as 
	|              |
	| [[result]] = |

#Audit
@ignore
Scenario Outline: Enter a number or variable that does not exist as URL
	Given I have the url '<url>' with timeoutSeconds '<timeoutSeconds>'
	When the web request tool is executed	
	Then the result should contain the string '<Error>'
	And the execution has "AN" error
	And the debug inputs as  
	| URL   | Header | Time Out Seconds |
	| <url> |        | <timeoutSeconds> |
	And the debug output as 
	|              | 
	| [[result]] = | 
Examples: 
	| url                                                  | timeoutSeconds | Error                                                                            |
	| 88                                                   |                | Unable to connect to the remote server                                           |
	| [[y]]                                                |                | Invalid URI: The hostname could not be parsed                                    |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=15 | [[y]]          | Value [[y]] for TimeoutSeconds Text could not be interpreted as a numeric value. |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=15 | " "            | Value    for TimeoutSeconds Text could not be interpreted as a numeric value.    |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=15 | sdf            | Value sdf for TimeoutSeconds Text could not be interpreted as a numeric value.   |

Scenario Outline: Enter a URL to download html with variables and recordsets
	Given I have the url '<url>' with timeoutSeconds '<timeoutSeconds>'
	And I have the Header '<Header>'
	When the web request tool is executed 
	Then the result should contain the string '<result>' equals '<output>'
	And the execution has "NO" error
	And the debug inputs as  
	| URL   | Header | Time Out Seconds |
	| <url> |  <Header>      | <timeoutSeconds> |
	And the debug output as 
	|                     |
	| [[result]] = String |
	Examples:
	| url                                                                                      | Header | timeoutSeconds                     | result                           | output                                                                |
	| [[rs().st]] = http://tst-ci-remote:3142/Public/Wait?WaitSeconds=15                       |        | [[rec(1).set]] = 20                | [[rs(1).set]]                    | [[rs(1).set]] = <DataList><Result>Wait Successful</Result></DataList> |
	| [[rs(*).st]] = http://tst-ci-remote:3142/Public/Wait?WaitSeconds=110                     |        | [[c]] = 120                        | [[rs().set]]                     | [[rs(1).set]]= <DataList><Result>Wait Successful</Result></DataList>  |
	| [[rs(1).st]] = http://tst-ci-remote:3142/Public/Wait?WaitSeconds=110                     |        | [[rec().set]] = 0                  | [[rs([[int]]).set]], [[int]] = 3 | [[rs(3).set]] = <DataList><Result>Wait Successful</Result></DataList> |
	| [[rs([[int]]).st]] = http://tst-ci-remote:3142/Public/Wait?WaitSeconds=15  , [[int]] = 3 |        | [[rec(*).set]] = 20                | [[rs().set]]                     | [[rs(1).set]] = <DataList><Result>Wait Successful</Result></DataList> |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=15                                     |        | [[rec([[int]]).set]] = 20, [[int]] | [[rs(*).set]]                    | [[rs(1).set]]= <DataList><Result>Wait Successful</Result></DataList>  |
	| http://tst-ci-remote:3142/Public/Wait?WaitSeconds=15                                     |        | [[rec([[int]]).set]] = 20, [[int]] | [[rs(1).set]]                    | [[rs(1).set]]= <DataList><Result>Wait Successful</Result></DataList>  |
	