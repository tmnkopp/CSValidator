Imports System.Text
Imports CSValidator
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Telerik.Web.UI

<TestClass()> Public Class TestValidator

    <TestMethod()> Public Sub Validator_Throws_Error_Any()
        Dim rtb As RadTextBox = New RadTextBox()
        rtb.Text = "555-555-aaaa"
        rtb.ID = "rtbTelephoneNumber"

        Dim errMessage As String
        Dim validator As ValidationBuilder = New ValidationBuilder(rtb)
        With validator
            .AddValidation("PHONE")
        End With
        If Not validator.IsValid() Then
            errMessage = validator.ErrorMessage
        End If
        Assert.IsNotNull(errMessage)
    End Sub
    Function ContainsABCD(ByVal StringToValidate As String) As Boolean
        If StringToValidate.Contains("ABCD") Then
            Return True
        End If
        Return False
    End Function

    '
End Class