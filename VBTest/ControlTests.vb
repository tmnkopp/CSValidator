Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports CSValidator
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Telerik.Web.UI

<TestClass()> Public Class ControlTests

    <TestMethod()> Public Sub ValidateAll()
        Dim rtb As RadTextBox = New RadTextBox()
        rtb.Text = "555-555-5555"
        rtb.ID = "rtbPhoneNumber"

        Dim tb As TextBox = New TextBox()
        tb.Text = "123.123.123.123"
        tb.ID = "IPAddress"

        Dim ddl As DropDownList = New DropDownList()
        ddl.SelectedValue = "555-555-5555"
        ddl.ID = "ddlPhoneNumbers"

        Dim validator As Validator = New Validator(rtb)
        Dim IsValid As Boolean = validator.Validate("REQUIRE")

        Assert.IsTrue(IsValid)
    End Sub
End Class