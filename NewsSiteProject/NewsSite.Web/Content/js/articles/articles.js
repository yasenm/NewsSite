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
            var linkRelOldImage = $('<link rel="image_src" />');
            $(linkRelOldImage).attr('href', image);
            var appId = $('<meta property="fb:app_id" />');
            $(appId).attr('content', '811616282251509');
            var contentType = $('<meta property="og:type" />');
            $(contentType).attr('content', 'article');
            var titleContent = $('<meta property="og:title" />');
            $(titleContent).attr('content', title);
            var imageContent = $('<meta property="og:image" />');
            $(imageContent).attr('content', image);
            var urlContent = $('<meta property="og:url" />');
            $(urlContent).attr('content', url);
            $('head')
                .append(linkRelOldImage)
                .append(appId)
                .append(contentType)
                //.append(titleContent)
                //.append(imageContent)
                .append(urlContent);
        }
    }
}());