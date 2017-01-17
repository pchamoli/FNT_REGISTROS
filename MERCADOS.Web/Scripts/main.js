$( document ).ready(function() {
    $(".tabla-result").hide();
	$("#form-inicial").show();
});


var openModal = function(url){
	$("#general-modal").load(url);
	$("#general-modal").modal();
}

var confirmar = function(){
	var rpta = confirm("¿Estas seguro de que desea grabar estos datos?");
	if (rpta == true) {
	    console.log("Confirmo");
	    $(".tabla-result").show();
		$("#form-inicial").hide();
	} else {
	    console.log("No confirmo");
	}
}

var cerrasesion = function(){
	console.log("Salir");
}

var openModalFormato = function(){
	var modal = $("#form").val();
	if(modal == "mayor"){
		openModal("modals/item-mayorista.html");
	}else{
		openModal("modals/item-minorista.html");
	}
}