
function getAboutInfo() {
	console.log("get about");
	$.get("/api/About/", function (data) {
		console.log("get about", data);
		$("#about").text(data);
	})
}

function ExecTranslate() {
	console.log("translate");
	var langfrom = document.getElementById("langfrom").value;
	var langto = document.getElementById("langto").value;
	var textElement = document.getElementById("text");
	var text = textElement.value;

	console.log("lang, text:", lang, text);
	var params = "?langfrom=" + langfrom + "&langto=" + langto + "&text=" + text;
	var url = "/api/Translate" + params
	console.log("translate url:", url);
	$.get(url, function(data){
		console.log("translation", data);
		$("#translation").text(data);
	})

}

window.addEventListener("load", (event) => {
	getAboutInfo();

	})
	
