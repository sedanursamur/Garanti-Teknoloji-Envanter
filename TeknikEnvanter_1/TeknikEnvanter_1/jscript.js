var data =
    '[{	"id": "1","name": "Gökhan Türkmen","tckn": "11111111111","phone": "05333333333" ,"position":"Team Leader", "birthPlace":"Ankara","itemNo": "21", "unit":"Ödeme Sistemleri"},' +
    '{"id": "3","name": "Selim Yıldız","tckn": "22222222222","phone": "05344444444" ,"position":"Developer","birthPlace":"Tokat","itemNo": "34", "unit":"Ödeme Sistemleri"},' +
    '{"id": "4","name": "Orhan Işık","tckn": "33333333333","phone": "05365444444","position":"Developer","birthPlace":"İstanbul","itemNo": "45", "unit":"Ödeme Sistemleri"},' +
    '{"id": "2","name": "Fatma Görmüş","tckn": "44444444444","phone": "05453222222","birthPlace":"Ankara",	"itemNo": "43", "unit":"Dokümantasyon","position":"Team Leader"},' +
    '{"id": "5","name": "Sedanur Samur","tckn": "55555555555","phone": "05877776655","birthPlace":"Diyarbakır",	"itemNo": "76", "unit":"Dokümantasyon","position":"Developer"},' +
    '{"id": "6","name": "Şenay Tuncel","tckn": "66666666666","phone": "05111111112","birthPlace":"İzmir","itemNo": "12", "unit":"Dokümantasyon","position":"Developer"	}]';

response = $.parseJSON(data);
var counter = 0;
$(document).ready(function () {
    $.each(response, function (i, item) {

        if (item.position == "Team Leader") {
            debugger;
            counter++;
            var $tr = $('<tr >').append(
                $('<td> ').html('<a href="#calisan' + counter + '" name="' + counter + '" class="oncol " id="collapse"> + </a> ' + item.name),
                $('<td>').html('<a href="#anaEkran" rel="modal:open" id="popupOpener">' + item.tckn + '</a>'),
                $('<td>').html('<a href="tel:+9' + item.phone + '">' + item.phone + '</a>')
            );
        }
        else {
            var $tr = $('<tr id="element' + counter + '" class="secure' + counter + '" name="secure"  style="display:none">').append(
                $('<td>').html(" " + item.name),
                $('<td>').html('<a href="#anaEkran" rel="modal:open" id="popupOpener">' + item.tckn + '</a>'),
                $('<td>').html('<a href="tel:+9' + item.phone + '">' + item.phone + '</a>')
            );
        }
        $('#records_table tbody').append($tr);


    });

    $(document).on("click", "#collapse", function (f) {

        var name = $(this).attr("name");
        var list = document.getElementsByClassName("secure" + name);

        var item = $("#element" + name).attr("style");

        if (item == "display:none") {

            $(this).html("-");
            for (var i = 0; i < list.length; i++) {
                list[i].setAttribute("style", "background: #cfd1cf");
            }
        }
        else {
            $(this).html("+");
            for (var i = 0; i < list.length; i++) {
                list[i].setAttribute("style", "display:none");
            }
        }
    });


    $(document).on("click", "#popupOpener", function (f) {

        var tc = $(this).text();

        $.each(response, function (i, item) {
            if (item.tckn === tc) {
                $('#popupName').html(item.name);
                $('#popupTc').html(item.tckn);
                $('#popupPlace').html(item.itemNo);
                $('#popupBirth').html(item.birthPlace);
                return;
            }
        });

    });


});

