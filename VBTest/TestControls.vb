Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports CSValidator
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Telerik.Web.UI

<TestClass()> Public Class TestControls
    <TestMethod()> Public Sub ValidateAllRadTextBox()
        Dim validator As Validator = New Validator()
        Dim bl_Errors As BulletedList = New BulletedList()
        Dim isvalid As Boolean = False
        Dim err As String

        Dim control As Control = GetControl()

        If Not validator.Validate(control).Phone().IsValid Then
            err = validator.ErrorMessage
        End If
        Assert.IsFalse(String.IsNullOrWhiteSpace(err))
    End Sub

    Function GetControl() As Control
        Dim control As RadTextBox = New RadTextBox()
        control.Text = "555-555-5555"
        control.ID = "rtbPhoneNumber"

        Dim cb As DropDownList = New DropDownList()
        cb.SelectedValue = "555-555-5555"
        cb.ID = "rtbPhoneNumber"

        Return DirectCast(cb, Control)
    End Function
End Class