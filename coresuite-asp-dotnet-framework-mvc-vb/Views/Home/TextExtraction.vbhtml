﻿@Code
    ViewData("Title") = "Text Extraction"
    Layout = "~/Views/Shared/_DynamicPDFLayout.vbhtml"
End Code

<h2>Extract Text from a PDF</h2>

<form name="FormFill" action="ExtractText" method="post">

    <input id="btnSubmit" type="submit" value="Click To View Extracted Text" />
</form>