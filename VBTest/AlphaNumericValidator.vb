
Imports CSValidator
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Telerik.Web.UI

Public Class AlphaNumericValidator
    Inherits ExpressionValidator
    Sub New()
        MyBase.New("AlphaNumeric")
    End Sub
End Class

<TestClass()> Public Class TestValidators
    <TestMethod()> Public Sub TestAlphaNumericValidator()
        Dim cb As DropDownList = New DropDownList()
        cb.SelectedValue = "555-555-5555"
        cb.ID = "rtbPhoneNumber"

        Dim validator As Validator = New Validator()
        Dim isvalid As Boolean = False
        Dim err As String

        If Not validator.Validate(cb).ApplyValidator(New AlphaNumericValidator()).IsValid Then
            err = validator.ErrorMessage
        End If
        Assert.IsFalse(String.IsNullOrWhiteSpace(err))
        Assert.IsFalse(False)
    End Sub
End Class

