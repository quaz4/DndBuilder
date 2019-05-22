/* TODO
* Function that wraps a GET request, taking a url and a callback
*   
* url: The address to make the GET requst at
* callback: The function to be called upon the completion of the request,
* is is passed a results object that either contains an error or data
*/
function get(url) {
    return new Promise((resolve, reject) => {
        let request = new XMLHttpRequest();
    
        request.open("GET", url, true);

        request.onreadystatechange = () => {

            if (request.readyState == 4) {
                if (request.status == 200) {
                    resolve(JSON.parse(request.responseText));
                } else {
                    reject({ error: request.status});
                }
            }
        };
        
        request.send();
    });
}

// Utility function to simplify fetching elements from the DOM by id
function $(id) {
    return document.getElementById(id);
} 