function findCt(strObjectName) {
    return document.getElementById(strObjectName)
}
jQuery(document).ready(function () {
    //callSlider($('#promotion'), true);
    $('.quickview-link').fancybox({
        scrolling: 'no',
    });

    $('.category-ul-rght .has-sub-cat').on('click', function () {
        var t = $(this);
        $('.category-ul-rght .has-sub-cat').removeClass('active');
        $('.category-ul-rght .has-sub-cat').find('ul').hide();

        if (!t.hasClass('active')) {
            t.addClass('active');
            t.find('ul').show();
        } else {
            t.removeClass('active');
            t.find('ul').hide();
        }
    });
});

$("#btnSend").click(function () {
    var ob;
    ob = findCt('txtEditor');
    if (ob.value == "") {
        alert("Nội dung bình luận không được trống !");
        ob.focus();
        return false
    }
    ob = findCt('txtName');
    if (ob.value == "") {
        alert("Vui lòng nhập Tên của Bạn !");
        ob.focus();
        return false
    }
    var obj = {};
    obj.Content = findCt('txtEditor').value;
    obj.Name = findCt('txtName').value;
    obj.Email = findCt('txtEmail').value;
    obj.Id = findCt('IdReply').value;
    obj.IdControl = findCt('IdControlReply').value;
    obj.IdNews = findCt('IdNews').value;
    obj.Rate = findCt('starrating').value;
    if (obj.Name == '' && obj.Content == '') return;
    var jsonData = JSON.stringify(obj);
    $.ajax({
        url: '/NewsComment.submit',
        type: 'POST',
        data: jsonData,
        success: function (data) {
            findCt('returnProcess').innerHTML = data;
        }
    });
    findCt('txtEditor').value = '';
    findCt('IdReply').value = '';
    findCt('IdControlReply').value = '';
    findCt('SpanReply').innerHTML = '';
});

function CallFormAnserClick(id, idControl, name) {
    findCt('IdReply').value = id;
    findCt('IdControlReply').value = idControl;
    findCt('txtEditor').value = '@' + name + ': ';
    findCt('SpanReply').innerHTML = 'Bạn đang trả lời bình luận của: <b style="color:red">' + name + '</b>'
}

function LikeClick(id, likeNumber) {
    var obj = {};
    obj.Id = id;
    obj.LikeNum = likeNumber;
    var jsonData = JSON.stringify(obj);
    $.ajax({
        url: '/NewsCommentLikeNum.submit',
        type: 'POST',
        data: jsonData,
        success: function (data) {
            $("#IdNumLike" + id).text(data)
        }
    });
}
$('#aCmt').on('click', function (e) {
    e.preventDefault();
    $('html,body').animate({
        scrollTop: $('#comment').offset().top + 150
    }, 'slow');
});
jQuery(document).ready(function ($) {
    $('a[href^="#"]').on('click', function (event) {
        var target = $($(this).attr('href'));
        target.show();
        if (target.length) {
            event.preventDefault();
            $('html, body').animate({
                scrollTop: target.offset().top - 60
            }, 600);
        }
    });
});
$(document).ready(function () {
    $("#aShHi").click(function () {
        $(this).text($(this).text() == 'Ẩn' ? 'Hiện' : 'Ẩn');
        $(".lMluc").toggle(600);
    });
});