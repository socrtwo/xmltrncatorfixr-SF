Imports System.Resources

Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("XML Truncator-Fixer")> 
<Assembly: AssemblyDescription("In order to deal with the perceived lack of standards with HTML, XML was designed from get-go to be intolerant of errors. XML files are used for many purposes including storing text and data in Microsoft's new Office Open MS Office file formats. We can see how this need for XML precision plays out when there is one mistake in the document.xml file within the larger docx zip structure, Word will not display any text. This can be quite frustrating in a data recovery situation where partial recovery is better than nothing. XML Truncator-Fixer attempts to fix XML files for these kinds of conditions. It does so by identifying the first error within a file and then truncating the file 50 characters before. The extra truncation is sometimes needed because the XML validator used to find the error is often wrong and indicates an error only after several characters of bad XML have already been passed. This program was made by me, Paul Pruitt with VB.Net on Visual Studio Pro 2010 using the Net 2.0 library. As a validator, this program uses a pp compiled Perl program that employs the XML::Parser module to indicate where errors occur. To truncate the XML file XML Truncator-Fixer uses trunc.exe from the trunc project hosted in Google apps. Finally to add the right ending tags to the truncated XML file, this project uses xmllint from the Libxml2 project. 1. Future possibilities for this program are to add a loop which removes one character at a time from the error location along with the rest of the truncated file and then revalidate the XML until it passes, allowing for more complete data recovery. 2. Another possibility is to make an entire GUI front end to XMLLint. If you have corrupt MS or Open Office files and need help for manual recovery of text/data, I charge $22 for successful consultations. If you are unsatisfied with the data recovery, you owe me nothing. You can send comments, suggestions or criticism about this program or corrupt Office files to socrtwo@s2services.com. --Paul Pruitt --02/23/2012")> 
<Assembly: AssemblyCompany("S2 Services")> 
<Assembly: AssemblyProduct("XMLTruncatorFixer")> 
<Assembly: AssemblyCopyright("Copyright ©  2012")> 
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)> 

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("3406c53f-85a1-459a-9168-e1b2935bacd9")> 

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("0.5.0.1")> 
<Assembly: AssemblyFileVersion("0.5.0.1")> 

<Assembly: NeutralResourcesLanguageAttribute("en-US")> 