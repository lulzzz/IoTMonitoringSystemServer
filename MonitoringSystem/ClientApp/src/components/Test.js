import React, {PropTypes} from 'react';
import connect from 'react-redux-fetch';
 
class Test extends React.Component {
    static propTypes = {
        // injected by react-redux-fetch
        /**
         * @var {Function} dispatchAllPokemonGet call this function to start fetching all Pokémon
         */
        dispatchAllPokemonGet: PropTypes.func.isRequired,
        /**
         * @var {Object} allPokemonFetch contains the result of the request + promise state (pending, fulfilled, rejected)
         */
        allPokemonFetch: PropTypes.object
    };
 
    componentDidMount() {
        this.props.dispatchAllPokemonGet();
    }
 
    render() {
        const {allPokemonFetch} = this.props;
 
        if (allPokemonFetch.rejected) {
            return <div>Oops... Could not fetch Pokémon!</div>
        }
 
        if (allPokemonFetch.fulfilled) {
            return <ul>
                {allPokemonFetch.value.results.map(pokemon => (
                    <li key={pokemon.name}>{pokemon.name}</li>
                ))}
            </ul>
        }
 
        return <div>Loading...</div>;
    }
}
 
// connect(): Declarative way to define the resource needed for this component
export default connect([{
    resource: 'allPokemon',
    method: 'get', // You can omit this, this is the default 
    request: {
        url: 'http://pokeapi.co/api/v2/pokemon/'
    }
}])(Test);