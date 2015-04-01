APP['Articles'] = {};
APP.Articles = (function () {

    return {
        init: function () {
            $('.img_modal').on('click', function (e) {
                e.preventDefault();
                $("#modal_img_target").attr("src", $(this).find('img').attr('src'));
                $("#modal").removeClass("hide");
                $('#modal').modal('show');
            });
        },

        initFbShare: function (title, image, url) {
            var titleContent = $('<meta property="og:title" />');
            $(titleContent).attr('content', title);
            var imageContent = $('<meta property="og:image" />');
            $(imageContent).attr('content', image);
            var urlContent = $('<meta property="og:url" />');
            $(urlContent).attr('content', url);
            $('head').append(titleContent).append(imageContent).append(urlContent);
        }
    }
}());