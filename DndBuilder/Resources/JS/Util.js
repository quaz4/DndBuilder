/*
* Functions that handle basic HTTP actions
*   
* url: The address to make the request at
* Returns: A Promise
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

// Decode encoded html
// WARNING: Do not display any values returned from this function
// as they may be unsafe
function decodeHtml(html) {
    let txtArea = document.createElement("textarea");
    txtArea.innerHTML = html;
    return txtArea.value;
}