Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports CSValidator
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Telerik.Web.UI

<TestClass()> Public Class TestControls
    <TestMethod()> Public Sub ValidateAllRadTextBox()
        Dim rtb As RadTextBox = New RadTextBox()
        rtb.Text = "555-555-5555"
        rtb.ID = "rtbPhoneNumber"

        Dim validator As ValidationBuilder = New ValidationBuilder(rtb, "PHONE").Validate()
        Assert.IsTrue(validator.IsValid)
    End Sub
End Class