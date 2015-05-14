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
        var itemId = $(this).attr("data-id");
        var isPost = $(this).attr("data-type");
        var jsonObject = { itemId: itemId }
        var toHide = $(this);
        if(isPost === "true") {
            $.ajax({
                type: "POST",
                url: "/Post/RemovePost",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(jsonObject),
                success: function() { toHide.parent().parent().hide() },
                error: function(data) { console.log(data) }
            });
        } else {
            $.ajax({
                type: "POST",
                url: "/Post/RemoveComment",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(jsonObject),
                success: function () { toHide.parent().hide() },
                error: function (data) { console.log(data) }
            });
        }
    });

    $(".edit-post").click(function () {
        var itemId = $(this).attr("data-id");
        var isPost = $(this).attr("data-type");
        var anom = $(this).parents(".post-box").find("p").text();
        $(this).parents(".post-box").find(".post").html('<textarea class="form-control" id="edit-box" name="status-text" ></textarea>' +
				'<button class="" name="cancel" type="submit">Cancel</button>' +
                '<button class="edit-save" data-id="' + itemId + '" data-type="' + isPost + '" name="edit-save">Done Editing</button>');
        $("#edit-box").val(anom.text);
    });

    $(".edit-save").click(function () {
        alert("yolo");
        var itemId = $(this).attr("data-id");
        var isPost = $(this).attr("data-type");
        var jsonPostId = { itemId: itemId }
        if (isPost === "true") {
            $.ajax({
                type: "POST",
                url: "/Post/EditPost",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(jsonPostId),
                success: function () {
                    alert("yolo success");
                },
                error: function (data) { console.log(data) }
            });
        } else {
            $.ajax({
                type: "POST",
                url: "/Post/EditComment",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(jsonPostId),
                success: function () { alert("CommentVirkar") },
                error: function (data) { console.log(data) }
            });
        }
    });

    $(".report-status").click(function () {
        var itemId = $(this).attr("data-id");
        var isPost = $(this).attr("data-type");
        var jsonObject = { itemId: itemId, isPost: isPost }
            $.ajax({
                type: "POST",
                url: "/Post/ReportItem",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(jsonObject),
                success: function () { alert("virkarPost") },
                error: function (data) { console.log(data) }
            });
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
    likeClient.client.updateLikeCount = function(like) {
        var counter = $("[data-id='" + like.id + "'][data-type='" + like.type + "'] .like-count");
        $(counter).fadeOut(function() {
            $(counter).text(like.count);
            $(this).fadeIn();
            $(this).parent().blur(); //remove focus af takka
            if (like.count < 2) {
                $(this).parent().addClass("btn-warning-on"); //gerir 
            }
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


