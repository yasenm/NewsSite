APP['Comments'] = {};
APP.Comments = (function () {
    var deleteCommentBtnSelector = '.delete-comment-btn',
        deleteCommentUrl = APP.BuildUrl('comment/deletecomment?id='),
        listOfCommentsUlr = APP.BuildUrl('comment/commentsforarticle?articleId='),
        articleIdSelector = '#article-details-id',
        commentsSectionSelector = '#article-comments',

        _refreshArticleComments = function () {
            var articleId = $(articleIdSelector).val();
            APP.HttpRequester.getHTML(listOfCommentsUlr + articleId)
                .success(function (commentsHtml) {
                    $(commentsSectionSelector).html(commentsHtml);
                })
        }

    return {
        init: function () {
            $(deleteCommentBtnSelector).on('click', function () {
                var id = $(this).attr('id');
                APP.HttpRequester.postData(deleteCommentUrl + id)
                    .success(function (success) {
                        if (success) {
                            _refreshArticleComments();
                        }
                        else {
                            alert('Коментара не успя да се изтрие!')
                        }
                    });
            })

            $('.create-comment-content-area').val('');
            $('.create-comment-authorname-area').val('');
        }
    }
}());