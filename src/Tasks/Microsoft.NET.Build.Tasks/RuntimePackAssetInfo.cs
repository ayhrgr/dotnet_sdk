﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Build.Framework;

namespace Microsoft.NET.Build.Tasks
{
    internal class RuntimePackAssetInfo
    {
        public string SourcePath { get; set; }

        public string DestinationSubPath { get; set; }

        public AssetType AssetType { get; set; }

        public string PackageName { get; set; }

        public string PackageVersion { get; set; }

        public string PackageRuntimeIdentifier { get; set; }

        public static RuntimePackAssetInfo FromItem(ITaskItem item)
        {
            var assetInfo = new RuntimePackAssetInfo();
            assetInfo.SourcePath = item.ItemSpec;
            assetInfo.DestinationSubPath = item.GetMetadata(MetadataKeys.DestinationSubPath);

            if (!Enum.TryParse<AssetType>(item.GetMetadata(MetadataKeys.AssetType), ignoreCase: true, out AssetType parsedAssetType) ||
                (parsedAssetType != AssetType.Native && parsedAssetType != AssetType.Runtime))
            {
                throw new InvalidOperationException("Unexpected asset type: " + item.GetMetadata(MetadataKeys.AssetType));
            }
            assetInfo.AssetType = parsedAssetType;

            assetInfo.PackageName = item.GetMetadata(MetadataKeys.PackageName);
            assetInfo.PackageVersion = item.GetMetadata(MetadataKeys.PackageVersion);
            assetInfo.PackageRuntimeIdentifier = item.GetMetadata(MetadataKeys.RuntimeIdentifier);

            return assetInfo;
        }
    }
}
