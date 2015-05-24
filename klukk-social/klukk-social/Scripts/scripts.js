$(document).ready(function () {

    jQuery('.tabs .tab-links a').on('click', function (e) {
        
        var currentAttrValue = jQuery(this).attr('href');

        // Show/Hide Tabs
        jQuery('.tabs ' + currentAttrValue).show().siblings().hide();

        // Change/remove current tab to active
        jQuery(this).parent('li').addClass('active').siblings().removeClass('active');

        e.preventDefault();
    });


    $("#friend-content").on("click", ".friend-req", function () {
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

    $(".unfriend").click(function () {
        var friendId = $(this).val();
        var json = { friendId: friendId }
        var toHide = $(this);
        $.ajax({
            type: "POST",
            url: "/User/DeleteFriend",
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(json),
            success: function() {
                toHide.hide();
                $("#friend-content").html('<button type="button" value="' + friendId + '" class="btn btn-success btn-block friend-req">Senda vinabeiðni</button>');
            },
            error: function (data) { console.log(data) }
        });
    });

    $(".del-friend-req").click(function () {
        var userToAdd = $(this).val();
        var json = { Id: 0, ToUserId: userToAdd, FromUserId: "" }
        var toHide = $(this);
        $.ajax({
            type: "POST",
            url: "/User/CancelFriendRequest",
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(json),
            success: function () { toHide.hide(); },
            error: function (data) { console.log(data) }
        });
    });

    $("#reports").on("click", ".report-cancel", function () {
        var reportId = $(this).attr("data-id");
        var json = { reportId: reportId }
        var toHide = $(this).closest(".report-item");
        $.ajax({
            type: "POST",
            url: "/Post/IgnoreReport",
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(json),
            success: function() {
                $(toHide).hide();
            },
            error: function (data) { console.log(data) }
        });
    });

    $("#reports").on("click", ".report-delete", function () {
        var reportId = $(this).attr("data-id");
        var json = { reportId: reportId }
        var toHide = $(this).closest(".report-item");
        $.ajax({
            type: "POST",
            url: "/Post/DeleteReportedItem",
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(json),
            success: function () {
                $(toHide).hide();
            },
            error: function (data) { console.log(data) }
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
                success: function () { toHide.closest(".postpadd").hide() },
                error: function(data) { console.log(data) }
            });
        } else {
            $.ajax({
                type: "POST",
                url: "/Post/RemoveComment",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(jsonObject),
                success: function () { toHide.closest(".commentdialog").hide() },
                error: function (data) { console.log(data) }
            });
        }
    });

    $(".edit-post").click(function () {
        var itemId = $(this).attr("data-id");
        var isPost = $(this).attr("data-type");
        var post = $(this).parents(".post-box").find("input");
        var value = post.val();
        post.hide();
        $(this).parents(".post-box").find(".edit-container").html('<textarea class="form-control" id="edit-box" name="status-text" ></textarea>' +
				'<button class="edit-cancel" >Hætta við</button>' +
                '<button class="edit-save" data-id="' + itemId + '" data-type="' + isPost + '">Breyta</button>');
        document.getElementById("edit-box").value = value;
    });

    $(".postpadd").on("click", ".edit-cancel", function () {
        var eb = document.getElementById("edit-box");
        $(eb).closest(".post-box").find("p").show();
        $(eb).closest(".edit-container").html("");
    });

    $(".postpadd").on("click", ".edit-save", function () {
        var itemId = $(this).attr("data-id");
        var isPost = $(this).attr("data-type");
        var newPost = document.getElementById("edit-box").value;
        var jsonPostId = { itemId: itemId, newPost: newPost }
        if (isPost === "true") {
            $.ajax({
                type: "POST",
                url: "/Post/EditPost",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(jsonPostId),
                success: function (result) {
                    var eb = document.getElementById("edit-box");
                    $(eb).closest(".post-box").find("p").html(result);
                    $(eb).closest(".post-box").find("p").show();
                    $(eb).closest(".edit-container").html("");
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
                success: function(result) {
                    var eb = document.getElementById("edit-box");
                    $(eb).closest(".post-box").find("p").text(result);
                    $(eb).closest(".post-box").find("p").show();
                    $(eb).closest(".edit-container").html("");
                },
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
                success: function () { },
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
                $(this).parent().addClass("btn-warning-on");
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


