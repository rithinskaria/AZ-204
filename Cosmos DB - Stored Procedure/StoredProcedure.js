function insertAirportData(airportData) {
    var context = getContext();
    var container = context.getCollection();
    var response = context.getResponse();

    // Ensure that the airportData has an id field

    if (!airportData.id) {
        throw new Error('Error: Document must have an "id" field.');
    }


    // Query to check if a document with the same id already exists
    var query = {
        query: 'SELECT * FROM c WHERE c.id = @id',
        parameters: [{ name: '@id', value: airportData.id }]
    };


    // Execute the query within the same partition (same id)
    var isAccepted = container.queryDocuments(container.getSelfLink(), query, { partitionKey: airportData.id }, function (err, documents) {
        if (err) {
            throw new Error('Error querying documents: ' + err.message);
        }

        if (documents.length > 0) {
            // If document exists, return an error
            response.setBody('Error: A document with this ID already exists.');
        } else {
            // Insert the new document within the same partition
            var isInserted = container.createDocument(container.getSelfLink(), airportData, { partitionKey: airportData.id }, function (err, newDocument) {
                if (err) {
                    throw new Error('Error inserting document: ' + err.message);
                }
                response.setBody('Document inserted successfully: ' + JSON.stringify(newDocument));
            });

            if (!isInserted) {
                throw new Error('Failed to insert document.');
            }
        }
    });

    if (!isAccepted) {
        throw new Error('The query was not accepted by the system.');
    }
}
