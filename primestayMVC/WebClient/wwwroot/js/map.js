// GOOGLE API --------------------------------------------------------
// Ref: https://developers.google.com/maps/documentation/javascript/marker-clustering
// -------------------------------------------------------------------
var clusterMap;
var map;
var infoWindow;

function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 12,
        center: { lat: 57.0287001523496, lng: 9.995664227619658 },
    });
    infoWindow = new google.maps.InfoWindow({
        content: "Test",
        disableAutoPan: true,
    });

    // Add a marker clusterer to manage the markers.
    clusterMap = new markerClusterer.MarkerClusterer({ map });
}

// Create static locations on page load:
// Add some markers to the map.
//-----------------------------
//
// const labels = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
// var markers = locations.map((position, i) => {
//     const label = labels[i % labels.length];
//     const marker = new google.maps.Marker({
//         position,
//         label,
//     });
// 
//     // markers can only be keyboard focusable when they have click listeners
//     // open info window when marker is clicked
//     marker.addListener("click", () => {
//         infoWindow.setContent("HOTEL HERE with label: " + label);
//         infoWindow.open(map, marker);
//     });
//     return marker;
// });
//
// const locations = [];