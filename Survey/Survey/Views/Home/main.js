window.onload = function () {

    var radioButton = document.form1.getElementsById("other");
    var textArea = document.form1.getElementsById("textBox");

    radioButton.onclick = function () {
        //alert("Click");
        textArea.focus();
    }
    textArea.onfocus = function () {
        radioButton.checked = true;
    }
}