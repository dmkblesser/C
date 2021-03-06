﻿Imports System.Data
Partial Class CourseDetail
    Inherits System.Web.UI.Page
    Protected Sub Page_Init(sender As Object, e As EventArgs)
        Dim id As Integer = Request.QueryString("id")
        courseSqlDataSource.SelectParameters("eCourseID").DefaultValue = id
        Dim dv As DataView = CType(courseSqlDataSource.Select(DataSourceSelectArguments.Empty), DataView)
        If dv.Count > 0 Then
            CourseName.Text = dv(0)(0)
            Firm.Text = dv(0)(1)
            Instructor.Text = dv(0)(3) + " " + dv(0)(2)
            TopicHolder.Text = dv(0)(4)
            Price.Text = dv(0)(5)
            If DBNull.Value.Equals(dv(0)(6)) Then
                Description.Text = ""
            Else
                Description.Text = dv(0)(6)
            End If
            Status.Text = dv(0)(7)

            If Status.Text = "Y" Then
                Status.Text = "No"
                CartCourseButton.Text = "Not available"
                CartCourseButton.Enabled = False

            ElseIf Status.Text = "N" Then
                Status.Text = "Yes"
            End If
            If Session("id") Is Nothing Then
                CartCourseButton.Enabled = False
                NonMember.Text = "You are not currently signed in. Please sign in to add to your cart.Not a member? <a href=""\Default.aspx""> Click Here.</a>"
            End If
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    Protected Sub CartCourse_Click(sender As Object, e As EventArgs) Handles CartCourseButton.Click
        Dim productid As Integer = Request.QueryString("id")
        Dim userid As Integer = Session("id")
        Dim userDV As DataView = CType(userSqlDataSource.Select(DataSourceSelectArguments.Empty), DataView)

        If userDV.Count = 0 Then
            Dim curDate As String = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
            userSqlDataSource.InsertParameters("orderDate").DefaultValue = curDate
            userSqlDataSource.InsertParameters("userID").DefaultValue = userid
            userSqlDataSource.InsertParameters("orderStatusId").DefaultValue = 1
            userSqlDataSource.Insert()

        End If
        userDV = CType(userSqlDataSource.Select(DataSourceSelectArguments.Empty), DataView)
        courseSqlDataSource.InsertParameters("orderID").DefaultValue = userDV(0)(0)
        courseSqlDataSource.InsertParameters("productID").DefaultValue = productid
        courseSqlDataSource.InsertParameters("workshopID").DefaultValue = Nothing
        courseSqlDataSource.InsertParameters("quantity").DefaultValue = 1

        Dim count As Integer = courseSqlDataSource.Insert()

    End Sub
End Class
