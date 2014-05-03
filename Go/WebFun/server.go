package main

import (
	"encoding/json"
	"fmt"
	"github.com/gorilla/mux"
	"log"
	"net/http"
)

type Response map[string]interface{}

func (r Response) String() (s string) {
	b, err := json.Marshal(r)
	if err != nil {
		s = ""
		return
	}
	s = string(b)
	return
}

func LogRequest(f http.HandlerFunc) (r http.HandlerFunc) {
	r = func(w http.ResponseWriter, r *http.Request) {
		log.Printf("%s\r\n", r.URL.String())
		f(w, r)
	}
	return
}

func main() {
	r := mux.NewRouter()
	r.HandleFunc("/", LogRequest(mainHandler))
	// r.HandleFunc("/create/{id}", LogRequest(views.CreateHandler))
	// r.HandleFunc("/home", LogRequest(views.HomeHandler))
	// r.HandleFunc("/user/{id}/Tasks", LogRequest(views.TaskListHandler))
	// r.HandleFunc("/user/{id}/Tasks/{TaskId}", LogRequest(views.IndividualTaskHandler))
	r.HandleFunc("/json", jsonHandler)
	http.Handle("/", r)

	http.ListenAndServe("0.0.0.0:3333", nil)
}

func mainHandler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Hello Gorilla ;)")
}

func notMainHandler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprintf(w, "Hello again;)")
}

func jsonHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	fmt.Fprint(w, Response{"success": true, "message": "Hello!"})
	return
}
