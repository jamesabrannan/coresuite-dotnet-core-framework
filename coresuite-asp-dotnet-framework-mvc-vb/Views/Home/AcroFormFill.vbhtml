﻿@ModelType coresuite_asp_dotnet_framework_mvc_vb.Models.W9FormModel

@Code
    ViewData("Title") = "AcroFormFill"
    Layout = "~/Views/Shared/_DynamicPDFLayout.vbhtml"
End Code

<form id="FormFill" action="AcroFormFillW9" method="post">
    <h3>2014 W-9 AcroForm Fill</h3>
    <table border=0>
        <tr><td>Name:</td><td>@Html.TextBoxFor(Function(m) m.Name)</td></tr>
        <tr><td>Business Name:</td><td>@Html.TextBoxFor(Function(m) m.BusinessName)</td></tr>
        <tr>
            <td>Business Type:</td>
            <td>
                @code
                    For Each value In System.Enum.GetValues(GetType(coresuite_asp_dotnet_framework_mvc_vb.Models.BusinessType))
                        @Html.RadioButtonFor(Function(m) m.BusinessType, value)
                        @Html.Label(value.ToString())End code
                <br />
                @code
                Next
                End code

                <table style="border:0">
                    <tr>
                        <td style="width:50%;border:0">
                            Other:
                        </td>
                        <td style="border:0">
                            @Html.TextBoxFor(Function(m) m.OtherBusinessType)
                        </td>
                    </tr>
                    <tr>
                        <td style="width:50%;border:0">
                            Enter the tax classification:
                        </td>
                        <td style="border:0">
                            @Html.TextBoxFor(Function(m) m.TaxClassification)
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr><td>Address (number, street, and apt. or suite no.):</td><td>@Html.TextBoxFor(Function(m) m.Address)</td></tr>
        <tr><td>City, state and ZIP code:</td><td>@Html.TextBoxFor(Function(m) m.CityState)</td></tr>
        <tr><td>Requester's name and address (optional):</td><td>@Html.TextBoxFor(Function(m) m.RequestersNameAndAddress)</td></tr>
        <tr><td>List account number(s) here (optional):</td><td>@Html.TextBoxFor(Function(m) m.AccountNumbers)</td></tr>
        <tr><td>Social security number:</td><td>@Html.TextBoxFor(Function(m) m.SSN)</td></tr>
        <tr><td colspan=2 align=center><b>Or</b></td></tr>
        <tr><td>Employer identification number:</td><td>@Html.TextBoxFor(Function(m) m.EmployersID)</td></tr>
        <tr><td colspan=2 align=center><input id="btnAcroFill" type="submit" value="Create W9" /></td></tr>
    </table>
</form>