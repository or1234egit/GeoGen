﻿using System.Collections.Generic;
using GeoGen.Core.Configurations;
using GeoGen.Core.Constructions.Parameters;

namespace GeoGen.Core.Constructions.PredefinedConstructions
{
    public class IntersectionFromLines : PredefinedConstruction
    {
        public override IReadOnlyList<ConstructionParameter> ConstructionParameters { get; }

        public override IReadOnlyList<ConfigurationObjectType> OutputTypes { get; }

        public IntersectionFromLines()
        {
            ConstructionParameters = new List<ConstructionParameter>
            {
                new SetConstructionParameter(new ObjectConstructionParameter(ConfigurationObjectType.Line), 2)
            };

            OutputTypes = new List<ConfigurationObjectType>{ConfigurationObjectType.Point};
        }
    }
}