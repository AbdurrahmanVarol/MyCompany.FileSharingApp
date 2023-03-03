let loadData = () => {
    $("#accordionFlushExample").html("");
    $.ajax({
        type: "GET",
        url: "/folder/getfolders",
        success: function (data) {
            console.log(data)

            let mainFolder = data.find(p => p.userId == p.folderId);
            let x = function1(data, mainFolder.folderId)
            x += ` <ul class="list-group mt-3"> ${func2(mainFolder.files)} </ul>`
            $("#accordionFlushExample").append(x)
        }
    })
}
$(document).ready(function () {

    const exampleModal = document.getElementById('exampleModal')
    exampleModal.addEventListener('show.bs.modal', event => {
        const button = event.relatedTarget
        // Extract info from data-bs-* attributes
        const recipient = button.getAttribute('data-bs-whatever')
        // If necessary, you could initiate an AJAX request here
        // and then do the updating in a callback.
        //
        // Update the modal's content.
        console.log(recipient)
        const modalTitle = exampleModal.querySelector('.modal-title')
        const modalBodyInput = exampleModal.querySelector('.modal-body input')
        modalBodyInput.value = ""
        $.ajax({
            type: "GET",
            url: `/disposablelink/createlink?fileId=${recipient}`,
            success: function (data) {
                console.log(data)
                modalBodyInput.value = data
            }
        })
    })


    $('[data-toggle="tooltip"]').tooltip();
    $("#info").hide();
    loadData()
    $("#btnGetFolders").click(function () {

    })

})

let function1 = (data, parentFolderId = null) => {

    console.log('parentfolderid : ' + parentFolderId)

    let result = ``
    let array = data.filter(p => p.parentFolderId == parentFolderId)
    if (array) {
        $.each(array, (index, item) => {
            result += `
                                                                                 <div class="accordion-item">
                                                                                        <h2 class="accordion-header" id="flush-heading${item.folderId}">
                                                                                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapse${item.folderId}" aria-expanded="false" aria-controls="flush-collapse${item.folderId}">
                                                                                            <span id="basic-addon${item.folderId}">
                                                                                                <img src="/icons/folder-fill.svg" />
                                                                                            </span> <p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p> ${item.folderName}
                                                                                            </button>
                                                                                        </h2>
                                                                                        <div id="flush-collapse${item.folderId}" class="accordion-collapse collapse" aria-labelledby="flush-heading${item.folderId}" data-bs-parent="#${parentFolderId != null ? 'accordionFlushExample' + index : 'accordionFlushExample'}">
                                                                                            <div class="accordion-body">
                                                                                                    <div class="w-100 d-flex justify-content-between">
                                                                                                <span>
                                                                                                            ${item.folderDescription}
                                                                                                </span>
                                                                                                <span class="btn-group">
                                                                                                    <a href="/folder/updateFolder?folderId=${item.folderId}" class="btn btn-custom-edit">
                                                                                                        <span>
                                                                                                                    <img src="/icons/wrench-adjustable.svg" />
                                                                                                        </span> Edit
                                                                                                    </a>
                                                                                                                   <button class="btn btn-custom-delete" onclick="deleteFolder('${item.folderId}')">
                                                                                                        <span>
                                                                                                                    <img src="/icons/trash-fill.svg" />
                                                                                                        </span> Delete
                                                                                                    </button>
                                                                                                              <a class="btn btn-custom-download" href="/folder/addfolder?folderId=${item.folderId}">
                                                                                                                <span>
                                                                                                                                    <img src="/icons/iconfolderPlus.png" />
                                                                                                                </span>New Folder
                                                                                                            </a>
                                                                                                    <a class="btn btn-custom-download" href="/file/addFile?folderId=${item.folderId}">
                                                                                                        <span>
                                                                                                            <img src="/icons/upload.svg" />
                                                                                                        </span> Upload New File
                                                                                                    </a>
                                                                                                </span>
                                                                                            </div>
                                                                                                            ${data.some(p => p.parentFolderId == item.folderId) ?
                    '<div class="mt-3 accordion accordion-flush mt3">' +
                    function1(data.filter(p => p.parentFolderId == item.folderId), item.folderId) + '</div>' : ''}
                                                                                                      <ul class="list-group mt-3"> ${func2(item.files)} </ul>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>`
        })
    }



    return result
}

let func2 = (files) => {
    let result = ``
    $.each(files, (index, item) => {
        result += `

                                                            <li class="list-group-item">
                                                                <div class="d-flex w-100 justify-content-between">
                                                                    <span>
                                                                        <img src="/icons/file-earmark-text-fill.svg" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ${item.fileName}
                                                                    </span>
                                                                    <span class="btn-group">
                                                                        <button class="btn btn-custom-share" data-bs-toggle="modal" data-bs-target="#exampleModal" data-bs-whatever="${item.fileId}">
                                                                            <img src="/icons/share.svg" />
                                                                        </button>
                                                                                <button onclick="deleteFile('${item.fileId}')" class="btn btn-custom-delete">

                                                                                            <img src="/icons/trash-fill.svg" />
                                                                                </button>
                                                                        <a href="/file/updatefile?fileId=${item.fileId}" class="btn btn-custom-edit">
                                                                            Edit
                                                                        </a>
                                                                        <a href="/file/downloadFile?fileId=${item.fileId}" class="btn btn-custom-download">
                                                                            <img src="/icons/download.svg" />
                                                                        </a>
                                                                    </span>
                                                                </div>
                                                            </li>`
    })
    return result
}

let deleteFile = (fileId) => {
    $.ajax({
        type: "DELETE",
        url: `/file/deletefile?fileId=${fileId}`,
        success: function (data) {
            if (data.isSuccess) {
                $("#info").html("").append(data.result);
                $("#info").show();
                loadData()
            }
        }

    })
}

let deleteFolder = (folderId) => {
    $.ajax({
        type: "DELETE",
        url: `/folder/deletefolder?folderId=${folderId}`,
        success: function (data) {
            if (data.isSuccess) {
                $("#info").html("").append(data.result);
                $("#info").show();
                loadData()
            }
        }

    })
}
