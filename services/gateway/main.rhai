fn subgraph_request(service, request) {
    let subgraph_name = service.name;
    print("[Router] >> Request to [" + subgraph_name + "]");
    print("Query: " + request.subgraph.body.query);
}
