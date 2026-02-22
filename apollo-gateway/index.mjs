import { ApolloServer } from '@apollo/server';
import { startStandaloneServer } from '@apollo/server/standalone';
import { ApolloGateway, IntrospectAndCompose, RemoteGraphQLDataSource } from '@apollo/gateway';

const gateway = new ApolloGateway({
    supergraphSdl: new IntrospectAndCompose({
        subgraphs: [
            { name: 'person', url: 'http://localhost:5011/graphql' },
            { name: 'relationship', url: 'http://localhost:5012/graphql' },
            { name: 'asset', url: 'http://localhost:5013/graphql' },
            { name: 'balance', url: 'http://localhost:5014/graphql' },
        ],
    }),
    buildService({ name, url }) {
        return new RemoteGraphQLDataSource({
            url,
            willSendRequest({ request }) {
                console.log(`\x1b[33m[Gateway] >> Request to [${name}]\x1b[0m`);
                console.log("Query:", request.query);
                if (request.variables) {
                    console.log("Variables:", JSON.stringify(request.variables, null, 2));
                }
            }
        });
    }
});

const server = new ApolloServer({ gateway });

const { url } = await startStandaloneServer(server, {
    listen: { port: 4000 },
});

console.log(`🚀 Apollo Gateway ready at ${url}`);
