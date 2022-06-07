function download() {
    var text = document.getElementById("mytextarea").value;
    text = text.replace(/\n/g, "\r\n"); // To retain the Line breaks.
    var blob = new Blob([text], { type: "text/plain" });
    var anchor = document.createElement("a");
    anchor.download = "my-filename.txt";
    anchor.href = window.URL.createObjectURL(blob);
    anchor.target = "_blank";
    anchor.style.display = "none"; // just to be safe!
    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);
}

function dropfile(file) {
    var reader = new FileReader();
    reader.onload = function (e) {
        mytextarea.value = e.target.result;
    };
    reader.readAsText(file, "UTF-8");
}

mytextarea.ondrop = function (e) {
    e.preventDefault();
    var file = e.dataTransfer.files[0];
    dropfile(file);
};

