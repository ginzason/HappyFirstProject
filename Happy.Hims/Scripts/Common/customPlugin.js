(function ($) {
    // 숫자만 입력받기
    // $("#id").OnlyNum(); , $("#id").OnlyNum({ type: ',' }); 숫자 + 3자리마다 구분자
    $.fn.OnlyNum = function (options) {
        var defaults = {
            type: ""
        };
        var settings = $.extend({}, defaults, options);
        return this.each(function () {
            if (options != null) {
                defaults.type = options.type != null ? options.type : defaults.type;
            }
            $(this).on("keyup keypress", function () {
                var text = $(this).val().replace(/[^0-9]/g, '');
                if (defaults.type != '') {
                    text = text.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1" + defaults.type);
                }
                $(this).val(text);
            });
        });
    }
    //jquery ui 달력 한글화
    $.fn.Date = function (options) {
        var defaults = {
            type: "-"
        };
        var settings = $.extend({}, defaults, options);
        return this.each(function () {
            $(this).datepicker({
                closeText: '닫기',
                prevText: '이전달',
                nextText: '다음달',
                currentText: '오늘',
                monthNames: ['1월', '2월', '3월', '4월', '5월', '6월',
                '7월', '8월', '9월', '10월', '11월', '12월'],
                monthNamesShort: ['1월', '2월', '3월', '4월', '5월', '6월',
                '7월', '8월', '9월', '10월', '11월', '12월'],
                dayNames: ['일', '월', '화', '수', '목', '금', '토'],
                dayNamesShort: ['일', '월', '화', '수', '목', '금', '토'],
                dayNamesMin: ['일', '월', '화', '수', '목', '금', '토'],
                weekHeader: 'Wk',
                dateFormat: 'yy-mm-dd',
                firstDay: 0,
                isRTL: false,
                duration: 200,
                showAnim: 'show',
                showMonthAfterYear: true,
                yearSuffix: '년'
            });
            if (options != null) {
                defaults.type = options.type != null ? options.type : defaults.type;
            }
            $(this).mask("9999" + defaults.type + "99" + defaults.type + "99");
        });
    }

})(jQuery);