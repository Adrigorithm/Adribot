using Adribot.constants.enums;

namespace Adribot.helpers;

/**
 * A configuration used by <b>PokemonEndpointStringGeneration</b> to create endpoint strings.
 * <ul>
 *  <li><b>ShouldFetchAll</b> takes precedence over Id and Name.</li>
 *  <li><b>Id</b> is the numeric id of the resource to fetch</li>
 *  <li><b>Name</b> is the name of the resource to fetch.</li> 
 * </ul>
 */
public record EndpointStringConfiguration(
    PokemonEndpoint Endpoint,
    bool ShouldFetchAll,
    int Id,
    string Name
);
