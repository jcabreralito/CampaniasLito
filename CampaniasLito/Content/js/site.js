function viewPage() {
    document.getElementById('contenedor_carga').style.visibility = 'visible';
    document.getElementById('contenedor_carga').style.opacity = '1';

    swal({
        title: "<span></span>",
        html: true,
        showConfirmButton: false,
        //animation: true,
        //customClass: 'animated swing',
        background: '#B3C9D8',
        grow: 'fullscreen',
    });

    document.getElementById('extraLargeModal').style.zIndex = '999999';
    document.getElementById('mediumModal').style.zIndex = '999999';
    document.getElementById('smallModal').style.zIndex = '999999';
    document.getElementById('largeModal').style.zIndex = '999999';
    document.getElementById('mediumModalTienda').style.zIndex = '999999';
    document.getElementById('mediumModalTienda').style.zIndex = '999999';

};

function viewPageSwalIn() {

    document.getElementById('contenedor_carga').style.visibility = 'visible';
    document.getElementById('contenedor_carga').style.opacity = '1';

    //swal({
    //    title: "<span></span>",
    //    html: true,
    //    showConfirmButton: false,
    //    animation: true,
    //    customClass: 'animated swing',
    //    background: '#B3C9D8',
    //    grow: 'fullscreen',
    //    timer: 100,
    //});

    //document.getElementById('contenedor_carga').style.visibility = 'hidden';
    //document.getElementById('contenedor_carga').style.opacity = '0';

    document.getElementById('contenedor_carga').style.zIndex = '-1';
    document.getElementById('extraLargeModal').style.zIndex = '99999999';
    document.getElementById('mediumModal').style.zIndex = '99999999';
    document.getElementById('smallModal').style.zIndex = '99999999';
    document.getElementById('largeModal').style.zIndex = '99999999';
    document.getElementById('mediumModalTienda').style.zIndex = '99999999';
    document.getElementById('mediumModalTienda').style.zIndex = '99999999';


};

function viewPageSwalInCamp() {

    document.getElementById('contenedor_carga').style.visibility = 'visible';
    document.getElementById('contenedor_carga').style.opacity = '1';

    swal({
        title: "<span>Generando Campaña - Este proceso tardará varios minutos...</span>",
        html: true,
        showConfirmButton: false,
        //animation: true,
        //customClass: 'animated swing',
        background: '#B3C9D8',
        grow: 'fullscreen',
        timer: 5000,
    });

    document.getElementById('contenedor_carga').style.visibility = 'hidden';
    document.getElementById('contenedor_carga').style.opacity = '0';

    document.getElementById('extraLargeModal').style.zIndex = '99999999';
    document.getElementById('mediumModal').style.zIndex = '99999999';
    document.getElementById('smallModal').style.zIndex = '99999999';
    document.getElementById('largeModal').style.zIndex = '99999999';
    document.getElementById('mediumModalTienda').style.zIndex = '99999999';
    document.getElementById('mediumModalTienda').style.zIndex = '99999999';


};

function viewPageSwalInM() {

    document.getElementById('contenedor_carga').style.visibility = 'visible';
    document.getElementById('contenedor_carga').style.opacity = '1';

    document.getElementById('mediumModal').style.zIndex = '99999999';
    document.getElementById('mediumModalTienda').style.zIndex = '99999999';
    document.getElementById('smallModal').style.zIndex = '99999999';
    document.getElementById('extraLargeModal').style.zIndex = '99999999';
    document.getElementById('largeModal').style.zIndex = '99999999';
    document.getElementById('mediumModalTienda').style.zIndex = '99999999';


};

function viewPageSwalOut() {

    document.getElementById('contenedor_carga').style.visibility = 'hidden';
    document.getElementById('contenedor_carga').style.opacity = '0';

    swal({
        title: "<span></span>",
        html: true,
        showConfirmButton: false,
        //animation: true,
        //customClass: 'animated swing',
        background: '#B3C9D8',
        grow: 'fullscreen',
        timer: 100,
    });

    document.getElementById('extraLargeModal').style.zIndex = '999999';
    document.getElementById('mediumModal').style.zIndex = '999999';
    document.getElementById('smallModal').style.zIndex = '999999';
    document.getElementById('largeModal').style.zIndex = '999999';
    document.getElementById('mediumModalTienda').style.zIndex = '999999';
    document.getElementById('mediumModalTienda').style.zIndex = '999999';

};

function viewPageSwalOutM() {

    document.getElementById('contenedor_carga').style.visibility = 'hidden';
    document.getElementById('contenedor_carga').style.opacity = '0';

    document.getElementById('extraLargeModal').style.zIndex = '999999';
    document.getElementById('mediumModal').style.zIndex = '999999';
    document.getElementById('smallModal').style.zIndex = '999999';
    document.getElementById('largeModal').style.zIndex = '999999';
    document.getElementById('mediumModalTienda').style.zIndex = '999999';
    document.getElementById('mediumModalTienda').style.zIndex = '999999';

};

function viewPage2Camp() {

    document.getElementById('contenedor_carga').style.visibility = 'visible';
    document.getElementById('contenedor_carga').style.opacity = '1';

    swal({
        title: "<span>Generando Campaña - Este proceso tardará varios minutos...</span>",
        html: true,
        showConfirmButton: false,
        background: '#B3C9D8',
        timer: 30000,
    });

    document.getElementById('extraLargeModal').style.zIndex = '-1';
    document.getElementById('mediumModal').style.zIndex = '-1';
    document.getElementById('smallModal').style.zIndex = '-1';
    document.getElementById('largeModal').style.zIndex = '-1';
    document.getElementById('mediumModalTienda').style.zIndex = '-1';
    document.getElementById('mediumModalTienda').style.zIndex = '-1';

};

function viewPage2() {

    document.getElementById('contenedor_carga').style.visibility = 'visible';
    document.getElementById('contenedor_carga').style.opacity = '1';

    swal({
        title: "<span></span>",
        html: true,
        showConfirmButton: false,
        background: '#B3C9D8',
        timer: 2000,
    });

    document.getElementById('extraLargeModal').style.zIndex = '-1';
    document.getElementById('mediumModal').style.zIndex = '-1';
    document.getElementById('smallModal').style.zIndex = '-1';
    document.getElementById('largeModal').style.zIndex = '-1';
    document.getElementById('mediumModalTienda').style.zIndex = '-1';
    document.getElementById('mediumModalTienda').style.zIndex = '-1';

};

function viewPage2M() {

    document.getElementById('contenedor_carga').style.visibility = 'visible';
    document.getElementById('contenedor_carga').style.opacity = '1';

    document.getElementById('mediumModal').style.zIndex = '99999999';
    document.getElementById('mediumModalTienda').style.zIndex = '99999999';
    document.getElementById('smallModal').style.zIndex = '99999999';
    document.getElementById('extraLargeModal').style.zIndex = '99999999';
    document.getElementById('largeModal').style.zIndex = '99999999';
    document.getElementById('mediumModalTienda').style.zIndex = '99999999';

};

$(function () {
    $('.mat-input-outer label').click(function () {
        $(this).prev('input').focus();
    });
    $('.mat-input-outer input').focusin(function () {
        $(this).next('label').addClass('active');
    });
    $('.mat-input-outer input').focusout(function () {
        if (!$(this).val()) {
            $(this).next('label').removeClass('active');
        } else {
            $(this).next('label').addClass('active');
        }
    });
});

$(function () {
    $('.focus :input').focus();
});



var x, i, j, selElmnt, a, b, c;
x = document.getElementsByClassName("customselect");  /*look for any elements with the class "customselect":*/

for (i = 0; i < x.length; i++) {

    selElmnt = x[i].getElementsByTagName("select")[0];
    a = document.createElement("DIV");  /*for each element, create a new DIV that will act as the selected item:*/
    a.setAttribute("class", "select-selected");
    a.innerHTML = selElmnt.options[selElmnt.selectedIndex].innerHTML;
    x[i].appendChild(a);

    b = document.createElement("DIV"); /*for each element, create a new DIV that will contain the option list:*/
    b.setAttribute("class", "select-items select-hide");

    for (j = 0; j < selElmnt.length; j++) {

        c = document.createElement("DIV"); /*for each option in the original select element, create a new DIV that will act as an option item:*/
        c.innerHTML = selElmnt.options[j].innerHTML;
        c.addEventListener("click", function (e) {
            /*when an item is clicked, update the original select box, and the selected item:*/
            var y, i, k, s, h;
            s = this.parentNode.parentNode.getElementsByTagName("select")[0];
            h = this.parentNode.previousSibling;

            for (i = 0; i < s.length; i++) {

                if (s.options[i].innerHTML == this.innerHTML) {
                    s.selectedIndex = i;
                    h.innerHTML = this.innerHTML;
                    y = this.parentNode.getElementsByClassName("same-as-selected");
                    for (k = 0; k < y.length; k++) {
                        y[k].removeAttribute("class");
                    }
                    this.setAttribute("class", "same-as-selected");
                    break;
                }
            }
            h.click();
        });
        b.appendChild(c);
    }
    x[i].appendChild(b);
    a.addEventListener("click", function (e) {
        /*when the select box is clicked, close any other select boxes,
        and open/close the current select box:*/
        e.stopPropagation();
        closeAllSelect(this);
        this.nextSibling.classList.toggle("select-hide");
        this.classList.toggle("select-arrow-active");
    });
}
function closeAllSelect(elmnt) {
    /*a function that will close all select boxes in the document,
    except the current select box:*/
    var x, y, i, arrNo = [];
    x = document.getElementsByClassName("select-items");
    y = document.getElementsByClassName("select-selected");
    for (i = 0; i < y.length; i++) {
        if (elmnt == y[i]) {
            arrNo.push(i)
        } else {
            y[i].classList.remove("select-arrow-active");
        }
    }
    for (i = 0; i < x.length; i++) {
        if (arrNo.indexOf(i)) {
            x[i].classList.add("select-hide");
        }
    }
}
/*if the user clicks anywhere outside the select box,
then close all select boxes:*/
document.addEventListener("click", closeAllSelect);

/* Custom Select js end */