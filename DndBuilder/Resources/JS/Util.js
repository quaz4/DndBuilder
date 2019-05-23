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

function post(url, body) {
    return new Promise((resolve, reject) => {
        let request = new XMLHttpRequest();
    
        request.open("POST", url, true);
        request.setRequestHeader("Content-Type", "application/json");
        
        request.onreadystatechange = () => {

            if (request.readyState == 4) {
                if (request.status == 201) {
                    resolve();
                } else {
                    reject({ error: request.status});
                }
            }
        };
        
        request.send(JSON.stringify(body));
    });
}

function put(url, body) {
    return new Promise((resolve, reject) => {
        let request = new XMLHttpRequest();
    
        request.open("PUT", url, true);
        request.setRequestHeader("Content-Type", "application/json");
        
        request.onreadystatechange = () => {

            if (request.readyState == 4) {
                if (request.status == 200) {
                    resolve();
                } else {
                    reject({ error: request.status});
                }
            }
        };
        
        request.send(JSON.stringify(body));
    });
}

function deleteRequest(url) {
    return new Promise((resolve, reject) => {
        let request = new XMLHttpRequest();
    
        request.open("DELETE", url, true);

        request.onreadystatechange = () => {

            if (request.readyState == 4) {
                if (request.status == 200) {
                    resolve();
                } else {
                    reject({ error: request.status});
                }
            }
        };
        
        request.send();
    });
}