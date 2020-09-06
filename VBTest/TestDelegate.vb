Imports System.Text
Imports CSValidator
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class TestDelegate
    <TestMethod()> Public Sub TestDelegateFires()
        Dim validator As Validator = New Validator("String To Validate")
        validator.ApplyValidation(AddressOf Between10And20, "My Validation")
        Assert.IsTrue(validator.IsValid)
    End Sub
    Function Between10And20(ByVal StringToValidate As String) As Boolean
        If StringToValidate.Length > 10 And StringToValidate.Length < 20 Then
            Return True
        End If
        Return False
    End Function
End Class