
 function initMap() {
  const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 10,
    center: {lat: 57.0287001523496, lng: 9.995664227619658 },
  });
    const infoWindow = new google.maps.InfoWindow({
        content: "Test",
    disableAutoPan: true,
  });
    // Create an array of alphabetical characters used to label the markers.
    const labels = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  // Add some markers to the map.
  const markers = locations.map((position, i) => {
    const label = labels[i % labels.length];
    const marker = new google.maps.Marker({
        position,
        label,
    });

    // markers can only be keyboard focusable when they have click listeners
    // open info window when marker is clicked
    marker.addListener("click", () => {
        infoWindow.setContent("HOTEL HERE with label: " + label);
    infoWindow.open(map, marker);
    });
    return marker;
  });

    // Add a marker clusterer to manage the markers.
     new markerClusterer.MarkerClusterer({markers, map});
}

const locations = [
{lat: 57.0287001523496, lng: 9.995664227619658 },
{lat: 57.019169952241285, lng: 9.991544354907399 },
{lat: 57.02066504716014, lng: 10.00544892531127 },
{lat: 57.030288281507566, lng: 10.013345348009766 },
{lat: 57.040189203239194, lng: 9.9101768688403 },
{lat: 57.04084294483538, lng: 9.911378498381376 },
{lat: 57.04691285316308, lng: 9.933694475572775 },
{lat: 57.05195479197993, lng: 9.904340382497935 },
{lat: 57.03178292931326, lng: 9.882196066669547 },
{lat: 57.04383133135054, lng: 9.872411368977934 },
];

