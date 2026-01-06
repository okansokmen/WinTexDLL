Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports System.Runtime.InteropServices
Imports System.IO

Namespace UHFAPP.custom
	Partial Public Class SetR3Form
		Inherits Form

		Private list As New List(Of ParameterInfo)()
		Private listSpecification As New List(Of ParameterInfo)()
		Public Sub New()
			InitializeComponent()

		  ' string strData = "aa=1";
		  '  byte[] data = System.Text.Encoding.ASCII.GetBytes(strData);
		   ' int result = UHFAPI.UHFUploadUserParam(data, (short)(data.Length));

			ReadListSpecificationData()
			ReadFlashData()
			LoadData()
		End Sub

		Private Sub btnSet_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSet.Click
			If Not Me.btnSet.IsHandleCreated Then Return

			If list IsNot Nothing AndAlso list.Count > 0 Then
				Dim sb As New StringBuilder()
				For k As Integer = 0 To list.Count - 1
					Dim info As ParameterInfo = list(k)
					sb.Append(info.Name)
					sb.Append("=")
					sb.Append(info.Value)
					sb.Append(";")
				Next k
				Dim data() As Byte = System.Text.Encoding.ASCII.GetBytes(sb.ToString())
				Dim result As Integer = UHFAPI.UHFUploadUserParam(data, CShort(data.Length))
				If result = 0 Then
					MessageBox.Show("success")
					Return
				End If
			End If

			MessageBox.Show("fail")
		End Sub

		Private Sub LoadData()
			Dim index As Integer = 0
			lvParm.Items.Clear()
			For k As Integer = 0 To list.Count - 1
				Dim info As ParameterInfo=list(k)

				Dim lv As New ListViewItem()
				lv.Text = (index + 1).ToString()

				Dim itemName As New ListViewItem.ListViewSubItem()
				itemName.Name = "cName"
				itemName.Text = info.Name
				lv.SubItems.Add(itemName)

				Dim itemValue As New ListViewItem.ListViewSubItem()
				itemValue.Name = "cValue"
				itemValue.Text = info.Value
				lv.SubItems.Add(itemValue)

				Dim itemSpecification As New ListViewItem.ListViewSubItem()
				itemSpecification.Name = "cSpecification"
				itemSpecification.Text = info.Specification
				lv.SubItems.Add(itemSpecification)

				lvParm.Items.Insert(index, lv)
				index = index + 1
			Next k


		End Sub



		Private Sub ReadFlashData()
			 list.Clear()
			 Dim param(511) As Byte
			 Dim paramLen(9) As Short
			 UHFAPI.UHFDownloadUserParam(param, paramLen)

			 If param IsNot Nothing AndAlso param.Length > 0 AndAlso paramLen(0) > 0 Then
				 Dim strParam As String = System.Text.Encoding.ASCII.GetString(param, 0, paramLen(0))
				 Dim data() As String = strParam.Split(";"c)
				 For k As Integer = 0 To data.Length - 1
					 If data(k) IsNot Nothing AndAlso data(k).Contains("="c) Then
						 Dim temp() As String = data(k).Split("="c)
						 Dim info As New ParameterInfo()
						 info.Name = temp(0).Trim()
						 info.Value = temp(1).Trim()
						 info.Specification = GetSpecification(info.Name)
						 list.Add(info)
					 End If
				 Next k

			 End If

		End Sub
		Private Sub ModificationData(ByVal key As String, ByVal value As String)
			'if (value != null && value.Length > 0)
			If True Then
				For k As Integer = 0 To list.Count - 1
					Dim info As ParameterInfo = list(k)
					If info.Name = key Then
						info.Value = value.Trim()
					End If
				Next k
			End If
		End Sub


		'编辑参数
		Private Sub lvParm_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvParm.DoubleClick
			If Not Me.lvParm.IsHandleCreated Then Return

			If lvParm.SelectedItems.Count >= 0 Then
				Dim index As Integer= Me.lvParm.SelectedItems(0).Index
				Dim info As ParameterInfo = list(index)

				Me.Hide()
				Dim f As New UHFAPP.custom.R5ModifyParametersForm(info.Name, info.Value, info.Specification)
				f.StartPosition = FormStartPosition.CenterScreen
				Dim rsult As DialogResult=f.ShowDialog()
				Me.Show()
				If rsult = System.Windows.Forms.DialogResult.OK Then
					ModificationData(f.name_Conflict, f.value)
					LoadData()
				End If
				Return
			End If
		End Sub

		Private Class ParameterInfo
'INSTANT VB NOTE: The field name was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
			Private name_Conflict As String

			Public Property Name As String
				Get
					Return name_Conflict
				End Get
				Set(ByVal value As String)
					name_Conflict = value
				End Set
			End Property
'INSTANT VB NOTE: The field value was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
			Private value_Conflict As String

			Public Property Value As String
				Get
					Return Me.value_Conflict
				End Get
				Set(ByVal value As String)
					Me.value_Conflict = value
				End Set
			End Property
'INSTANT VB NOTE: The field specification was renamed since Visual Basic does not allow fields to have the same case-insensitive name as other class members:
			Private specification_Conflict As String

			Public Property Specification As String
				Get
					Return specification_Conflict
				End Get
				Set(ByVal value As String)
					specification_Conflict = value
				End Set
			End Property


		End Class

		'读取注释配置文件
		Private Sub ReadListSpecificationData()
			listSpecification.Clear()
			Dim path As String = Environment.CurrentDirectory & "\DEVICE.CFG"
			Dim sr As New StreamReader(path, Encoding.Default)
			Dim temp As String = Nothing

			temp = sr.ReadLine()
'INSTANT VB WARNING: An assignment within expression was extracted from the following statement:
'ORIGINAL LINE: while ((temp = sr.ReadLine()) != null)
			Do While temp IsNot Nothing
				If temp.Contains("#") Then
					Dim info As New ParameterInfo()
					Dim str() As String = temp.Split("#"c)
					info.Name = str(0).Trim()
					info.Specification = str(1).Trim()
					listSpecification.Add(info)
				End If
				temp = sr.ReadLine()
			Loop
			sr.Close()
		End Sub
		'根据名称获取注释
		Private Function GetSpecification(ByVal name As String) As String
			If listSpecification IsNot Nothing AndAlso listSpecification.Count > 0 Then
				For k As Integer = 0 To listSpecification.Count - 1
					If name = listSpecification(k).Name Then
						Return listSpecification(k).Specification
					End If
				Next k
			End If
			Return ""
		End Function

	End Class
End Namespace






  'private void WriteData(string key,string value)
  '      {
  '          StringBuilder sb = new StringBuilder();
  '          string path = System.Environment.CurrentDirectory + "\\DEVICE.CFG";
  '          StreamReader sr = new StreamReader(path, Encoding.Default);
  '          string temp = null;
  '          while ((temp = sr.ReadLine()) != null)
  '          {
  '              if (temp.Trim().StartsWith(key))
  '              {
  '                  sb.Append(key + "=" + value);

  '              }
  '              else
  '              {
  '                  sb.Append(temp);
  '              }
  '              sb.Append("\r\n");
  '          }
  '          sr.Close();

  '          StreamWriter sw = new StreamWriter(path, false);
  '          sw.Write(sb.ToString());
  '          sw.Close();
  '      }
