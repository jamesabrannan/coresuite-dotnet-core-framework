﻿@Code
    ViewData("Title") = "Select Pages"
    Layout = "~/Views/Shared/_DynamicPDFLayout.vbhtml"
End Code

<h2>Select Pages Example</h2>
<form id="selectPages" action="SelectPages1" method="post">
    <h3>Select the pages you want from Document A.</h3>
    @Html.CheckBox("A1", true) @Html.Label("A1", "Page 1") <br />
    @Html.CheckBox("A2", false) @Html.Label("A2", "Page 2") <br />
    @Html.CheckBox("A3", false) @Html.Label("A3", "Page 3") <br />
    @Html.CheckBox("A4", false) @Html.Label("A4", "Page 4") <br />

    <h3>Select the pages you want from Document B.</h3>
    @Html.CheckBox("B1", false) @Html.Label("B1", "Page 1") <br />
    @Html.CheckBox("B2", true) @Html.Label("B2", "Page 2") <br />
    @Html.CheckBox("B3", false) @Html.Label("B3", "Page 3") <br />

    <h3>Select the pages you want from Document C.</h3>
    @Html.CheckBox("C1", false) @Html.Label("C1", "Page 1") <br />
    @Html.CheckBox("C2", false) @Html.Label("C2", "Page 2") <br />

    <h3>Select the pages you want from Document D.</h3>
    @Html.CheckBox("D1", false) @Html.Label("D1", "Page 1") <br />
    @Html.CheckBox("D2", false) @Html.Label("D2", "Page 2") <br />
    @Html.CheckBox("D3", false) @Html.Label("D3", "Page 3") <br />
    @Html.CheckBox("D4", true) @Html.Label("D4", "Page 4") <br />
    @Html.CheckBox("D5", false) @Html.Label("D5", "Page 5") <br />
    @Html.CheckBox("D6", false) @Html.Label("D6", "Page 6") <br />

    <input id="btnSubmit" type="submit" value="Create Document" />
</form>