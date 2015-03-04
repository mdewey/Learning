package main

import (
	"io"
	"net/http"
	"github.com/gorilla/mux"
)

func hello(w http.ResponseWriter, r *http.Request) {
	io.WriteString(w, "Hello world!")
}

func setLunch(w http.ResponseWriter, r *http.Request) {
	//place := req.URL.Query().Get(":place")
	vars := mux.Vars(r) 
	place := vars["place"]
	io.WriteString(w, "setting lunch to" + place)
}

func main() {
	r := mux.NewRouter()
	r.HandleFunc("/", hello)
    r.HandleFunc("/lunch/place/{place}",setLunch)
	http.Handle("/", r)
	http.ListenAndServe(":9000", nil)
}
