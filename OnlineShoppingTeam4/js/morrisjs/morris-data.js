$(function () {
    //Recovers jsons and completes the tables in the dashboard
    $.ajax({
        url: 'MorrisArea',
        dataType: 'json',
        cache: false,
        success: function (data) {
            Morris.Area({
                element: 'morris-area-chart',
                data: data,
                xkey: ['Year'],
                ykeys: ['Sales'],
                labels: ['Sales'],
                pointSize: 2,
                hideHover: 'auto',
                resize: true
            })

        }
    });
    $.ajax({
        url: 'MorrisDonut',
        dataType: 'json',
        cache: false,
        success: function (data) {
            Morris.Donut({
                element: 'morris-donut-chart',
                data:data,
                resize: true
            })
        }

    })
    $.ajax({
        url: 'MorrisBar',
        dataType: 'json',
        cache: false,
        success: function (data) {
            Morris.Bar({
                element: 'morris-bar-chart',
                data: data,
                xkey: ['Year'],
                ykeys: ['dashboardMorrisBarColumA', 'dashboardMorrisBarColumB', 'dashboardMorrisBarColumC'],
                labels: ['Aprilie', 'August', 'Decembrie'],
                hideHover: 'auto',
                resize: true
            })
        }

    })
});
