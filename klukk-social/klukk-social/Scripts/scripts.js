$(document).ready(function () {
    $(".friend-req").click(function() {
        var userToAdd = $(this).val();
        var json = {Id: 0, ToUserId: userToAdd, FromUserId: ""}
        var toHide = $(this);
        $.ajax({
                type: "POST",
                url: "/User/SendFriendRequest",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(json),
            success: function () { toHide.hide(); },
            error: function(data) { console.log(data) }
        });
    });

    $(".accept-friend").click(function () {
        var fromUser = $(this).val();
        var json = { Id: 0, ToUserId: "", FromUserId: fromUser }
        var toHide = $(this);
        $.ajax({
            type: "POST",
            url: "/User/AcceptFriendRequest",
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(json),
            success: function() { toHide.parent().hide()},
            error: function (data) { console.log(data) }
        });
    });

    $(".delete-post").click(function () {
        var postId = $(this).attr("data-id");
        var isPost = $(this).attr("data-type");
        var jsonPostId = { postId: postId }
        var toHide = $(this);
        if(isPost) {
            $.ajax({
                type: "POST",
                url: "/Post/RemovePost",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(jsonPostId),
                success: function() { toHide.parent().parent().hide() },
                error: function(data) { console.log(data) }
            });
        } else {
            alert("BLABLA");
            $.ajax({
                type: "POST",
                url: "/Post/RemoveComment",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(jsonPostId),
                success: function () { toHide.parent().hide() },
                error: function (data) { console.log(data) }
            });
        }
    });
    
    $('.like-button').hover(
	// Handles the mouseover
	function () {
		$(this).andSelf().addClass('btn-warning');
	},
	// Handles the mouseout
	function () {
		$(this).andSelf().removeClass('btn-warning');
	});
});

$(function () {
    var likeClient = $.connection.likeHub;
    likeClient.client.updateLikeCount = function (like) {
        var counter = $("[data-id='" + like.id + "'][data-type='" + like.type + "'] span");
        $(counter).fadeOut(function () {
            $(counter).text(like.count);
            $(this).fadeIn();
        });
    };
    $(".like-button").on("click", function () {
        var code = $(this).attr("data-id");
        var user = $(this).attr("data-user");
        var type = $(this).attr("data-type");
        likeClient.server.like(code, user, type);
    });

    $.connection.hub.start();

});