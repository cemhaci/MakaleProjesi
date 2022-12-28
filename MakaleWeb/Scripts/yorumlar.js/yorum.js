var NotId = -1;
$(function () {     //bu function ımız sayfa yklenriken çalışacaktır

    $('#modal1').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        NotId = button.data('whatever')

        //$('#modal1').on('show.bs.modal', function (e) {
        //    console.log("test");
        //    var button = $(e.relatedTarget);   //reletedtarget tıklanaan şeyin bilgisi demek.yani butonun
        //    console.log("test");
        //    var NotId = button.data("NotId");
        //    console.log(NotId);
        $('#modal1_body').load("/Yorum/YorumGoster/" + NotId);
        console.log("test");
    });
});
function yorumislem(btn, islem, yorumid, yorumtext)   //bu function ise ne zaman çağırırsak o zaman çalışacaktır
{
    var button = $(btn);  //burada butonu yakaladı
    var editmod = button.data('edit');  //butonun edit datasını al. data edit tin içinde false olarak string bir ifadeyle yazdık fakat var buarada stringin içindeki değeri alma görevinde bulunuyor

    if (islem === 'update') {
        if (!editmod) {
            button.data('edit', true);
            button.removeClass('btn-warning');
            button.addClass('btn-success');
            var span = button.find('span');
            span.removeClass('glyphicon-edit');
            span.addClass('glyphicon-ok');

            $(yorumtext).attr("contenteditable", true);  //yorumtext=partialpage e gönderdiğimiz text in bilgisi. attr=attribute
        }
        else {
            button.data('edit', false);
            button.removeClass('btn-success');
            button.addClass('btn-warning');
            var span = button.find('span');
            span.removeClass('glyphicon-ok');
            span.addClass('glyphicon-edit');

            $(yorumtext).attr("contenteditable", false);
            var yenitxt = $(yorumtext).text();
            $.ajax({
                method: "Post",
                url: "/yorum/edit/" + yorumid,
                data: { text: yenitxt }  //text burada parametre bunu edit controller da alıcaz

            }).done(function (data) {    //yorum contreller da ki edit de return edilen bilgi data ya geliyor
                //done olunca yorumlar yeniden yüklenir
                if (data.sonuc)
                    $('#modal1_body').load("/Yorum/YorumGoster/" + NotId);
                else
                    alert("yorum düzenlenemedi");
            }).fail(function () {
                alert('sunucu ile bağlatı kurulamadı');
            })
        }
    }
    else if (islem === 'delete') {
        var mesaj = confirm("yorum silinsin mi?");
        if (!mesaj)
            return false;
        $.ajax({
            method: "Post",
            url: "/yorum/delete/" + yorumid
        }).done(function () {
            $('#modal1_body').load("/Yorum/YorumGoster/" + NotId);
        }).fail(function () {
            alert("sunucu ile bağlantı kurulamadı");
        })
    }
    else if (islem === 'create') {
        var yorum = $('#yorum_text').val();
        $.ajax({
            method: 'Post',
            url: "/yorum/create/",
            data: { Text: yorum, notid: NotId }
        }).done(function (data) {    //yorum contreller da ki edit de return edilen bilgi data ya geliyor
            //done olunca yorumlar yeniden yüklenir
            if (data.sonuc)
                $('#modal1_body').load("/Yorum/YorumGoster/" + NotId);
            else
                alert("yorum düzenlenemedi");
        }).fail(function () {
            alert('sunucu ile bağlatı kurulamadı');
        })
    }
}