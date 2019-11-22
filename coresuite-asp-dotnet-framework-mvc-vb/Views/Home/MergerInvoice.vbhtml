﻿@Code
    ViewData("Title") = "Merger Invoice"
    Layout = "~/Views/Shared/_DynamicPDFLayout.vbhtml"
End Code

<form name="FormFill" action="CreateMergerInvoice" method="post">
    <h2>Select the invoices to include in the PDF.</h2>
    @Html.CheckBox("10248", false) @Html.Label("10248", "10248") <br />
    @Html.CheckBox("10249", false) @Html.Label("10249", "10249") <br />
    @Html.CheckBox("10250", false) @Html.Label("10250", "10250") <br />
    @Html.CheckBox("10251", false) @Html.Label("10251", "10251") <br />
    @Html.CheckBox("10252", false) @Html.Label("10252", "10252") <br />
    @Html.CheckBox("10360", true) @Html.Label("10360", "10360") <br />
    @Html.CheckBox("10979", true) @Html.Label("10979", "10979") <br />
    @Html.CheckBox("11077", false) @Html.Label("11077", "11077") <br />

    <input id="btnSubmit" type="submit" value="Create Invoices" />
</form>