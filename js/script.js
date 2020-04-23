
    function  fileUploaded = function (sender, args) {
        var id = args.get_fileInfo().ImageID;

        $(".imageContainer")
            .html("")

        $(".info").html(String.format("<strong>{0}</strong> successfully inserted.<p>Record ID - {1}</p>", args.get_fileName(), id));

    }

    function getImageUrl = function (id) {
        var url = window.location.href;
        var handler = "StreamImage.ashx?imageID=" + id;
        var index = url.lastIndexOf("/");
        var completeUrl = url.substring(0, index + 1) + handler;
        return completeUrl
    }

    function fileUploadRemoving = function (sender, args) {
        var index = args.get_rowIndex();
        $(".imageContainer img:eq(" + index + ")").remove();
    }
