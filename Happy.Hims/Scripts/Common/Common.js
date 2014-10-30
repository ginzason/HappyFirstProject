var Common = {
    GoPage: function (page) {
        var fulllUrl = location.href;
        var arrFullUrl = fulllUrl.split('?');
        var url = arrFullUrl[0];
        var parameter = '';
        if (arrFullUrl.length > 1) {
            var arrParam = arrFullUrl[1].split('&');
            for (var i = 0; i < arrParam.length; i++) {
                var arrDparam = arrParam[i].split('=');
                if (arrDparam[0] == 'page') {
                    parameter += parameter != '' ? '&page=' + page : '?page=' + page;
                }
                else {
                    parameter += parameter != '' ? '&' + arrDparam[0] + '=' + arrDparam[1] : '?' + arrDparam[0] + '=' + arrDparam[1];
                }
            }
            url += parameter;
        }
        else {
            url += '?page=' + page;
        }
        location.href = url;
    }
}

jQuery(function ($) {
    $.datepicker.regional['ko'] = {
        closeText: '닫기',
        prevText: '이전',
        nextText: '다음',
        currentText: '오늘',
        monthNames: ['1월', '2월', '3월', '4월', '5월', '6월', '7월', '8월', '9월', '10월', '11월', '12월'],
        monthNamesShort: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        dayNames: ['일', '월', '화', '수', '목', '금', '토'],
        dayNamesShort: ['일', '월', '화', '수', '목', '금', '토'],
        dayNamesMin: ['일', '월', '화', '수', '목', '금', '토'],
        weekHeader: 'Wk',
        dateFormat: 'yy-mm-dd',
        firstDay: 0,
        isRTL: false,
        showMonthAfterYear: true,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['ko']);

    $('#text_field').datepicker();

});