using System;
using System.Collections.Generic;
using System.Net;
using CiFarm.Core.Databases;
using CiFarm.RestApi;
using CiFarm.Utils;
using Cysharp.Threading.Tasks;

namespace CiFarm.GraphQL
{
    public partial class GraphQLClient
    {
        public async UniTask<Activities> QueryActivitiesAsync(string query = null)
        {
            var name = "activities";

            // If the query is null, use the default query with proper string interpolation
            query ??=
                $@"
query {{
    {name} {{
        water {{
            energyConsume
            experiencesGain
        }}
        feedAnimal {{
            energyConsume
            experiencesGain
        }}
        usePesticide {{
            energyConsume
            experiencesGain
        }}
        useFertilizer {{
            energyConsume
            experiencesGain
        }}
        harvestCrop {{
            energyConsume
            experiencesGain
        }}
        helpCureAnimal {{
            energyConsume
            experiencesGain
        }}
        helpUseHerbicide {{
            energyConsume
            experiencesGain
        }}
        helpUsePesticide {{
            energyConsume
            experiencesGain
        }}
        helpWater {{
            energyConsume
            experiencesGain
        }}
        thiefAnimalProduct {{
            energyConsume
            experiencesGain
        }}
        thiefCrop {{
            energyConsume
            experiencesGain
        }}
        useHerbicide {{
            energyConsume
            experiencesGain
        }}
    }}
}}";

            return await QueryAsync<Empty, Activities>(name, query);
        }
    }
}
