var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            url: '/admin/game/getall'
        },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'genre.name', "width": "10%" },
            { data: 'description', "width": "25%" },
            { data: 'studio.name', "width": "20%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="btn-group" role="group"> </div>
                        <a href="game/upsert?id=${data}" class="btn btn-primary mx-2">Edit</a>
                        <a onClick=Remove('game/remove?id=${data}') class="btn btn-danger mx-2">Delete</a>
                        `
                },
                "width": "20%"
            }
        ]
    });
}

function Remove(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}