$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tableData').DataTable({
        "ajax": {
            url: '/admin/game/getall'
        },
        "columns": [
            { data: 'title', "width": "20%" },
            { data: 'genre.name', "width": "20%" },
            { data: 'description', "width": "20%" },
            { data: 'studio.name', "width": "20%" }
        ]
    });
}