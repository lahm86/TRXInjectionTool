namespace TRXInjectionTool.Types.TR1.Lara;

public class TR1LaraBraidBuilder : InjectionBuilder
{
    public override void Build()
    {

    }

    /*
    static void EnhancedBraidExport()
        {
            // A textured braid plus edits to Lara's head to make it
            // appear more natural.

            TRLevel level = InjectionIO.CreateLevelBase(1);

            new TR1ModelImporter
            {
                Level = level,
                EntitiesToImport = new List<TREntities> { TREntities.LaraPonytail_H_U },
                DataFolder = @"Resources\Models"
            }.Import();

            // Additional meshes for Midas touch
            int i;
            for (i = 1; i < 256; i++)
            {
                if (level.Palette[i].Red == 0 && level.Palette[i].Green == 0 && level.Palette[i].Blue == 0)
                {
                    level.Palette[i].Red = 63;
                    level.Palette[i].Green = 59;
                    level.Palette[i].Blue = 34;
                    break;
                }
            }

            List<TRMesh> meshes = level.Meshes.ToList();
            foreach (TRMesh mesh in meshes)
            {
                TRMesh dupe = CloneMeshAsColoured(mesh, (ushort)i);
                TRMeshUtilities.InsertMesh(level, dupe);
            }

            List<TRMeshTreeNode> nodes = level.MeshTrees.ToList();
            nodes.AddRange(level.MeshTrees);
            level.MeshTrees = nodes.ToArray();
            level.NumMeshTrees *= 2;
            level.Models[0].NumMeshes *= 2;

            InjectionData data = InjectionData.CreateFromLevel(level);
            data.InjectionType = InjectionType.Braid;

            TRMeshEdit headEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.Lara,
                MeshIndex = 14,
                FaceEdits = new List<TRFaceTextureEdit>
                {
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 0,
                        FaceType = TRFaceType.TexturedQuad,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 1 }
                    },
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 5,
                        FaceType = TRFaceType.TexturedTriangle,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 66, 67, 68, 69, 70, 71, 72, 73 }
                    }
                },
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 45,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 44,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 43,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    }
                }
            };
            TRMeshEdit uziHeadEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.LaraUziAnimation_H,
                MeshIndex = 14,
                FaceEdits = new List<TRFaceTextureEdit>
                {
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 0,
                        FaceType = TRFaceType.TexturedQuad,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 6 }
                    },
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 5,
                        FaceType = TRFaceType.TexturedTriangle,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 56, 57, 58, 59, 60, 61, 62, 63 }
                    }
                },
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 45,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 44,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 43,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    }
                }
            };

            data.MeshEdits = new List<TRMeshEdit>
            {
                headEdit, uziHeadEdit
            };

            InjectionIO.Export(data, @"Output\braid.bin");
        }

        static void ValleyBraidExport()
        {
            // Edit the misc anim head to match the normal
            TRLevel level = InjectionIO.CreateLevelBase(0);


            InjectionData data = InjectionData.CreateFromLevel(level);
            data.InjectionType = InjectionType.Braid;

            TRMeshEdit miscHeadEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.LaraMiscAnim_H,
                MeshIndex = 14,
                FaceEdits = new List<TRFaceTextureEdit>
                {
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 0,
                        FaceType = TRFaceType.TexturedQuad,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 14, 16, 17, }
                    },
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 5,
                        FaceType = TRFaceType.TexturedTriangle,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 3, 37, 38, 39, 40 }
                    }
                },
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 45,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 44,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 43,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    }
                }
            };
            TRMeshEdit miscShotgunEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.LaraMiscAnim_H,
                MeshIndex = 7,
                FaceEdits = new List<TRFaceTextureEdit>(),
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 26,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 27,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 28,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 29,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                }
            };

            data.MeshEdits = new List<TRMeshEdit>
            {
                miscHeadEdit,miscShotgunEdit
            };

            InjectionIO.Export(data, @"Output\valley_braid.bin");
        } 

    static TRMesh CloneMeshAsColoured(TRMesh mesh, ushort paletteIndex)
        {
            TRMesh clone = new TRMesh
            {
                Centre = mesh.Centre,
                CollRadius = mesh.CollRadius,
                ColouredRectangles = new TRFace4[mesh.NumTexturedRectangles],
                ColouredTriangles = new TRFace3[mesh.NumTexturedTriangles],
                Lights = mesh.Lights,
                Normals = mesh.Normals,
                NumColouredRectangles = mesh.NumTexturedRectangles,
                NumColouredTriangles = mesh.NumTexturedTriangles,
                NumNormals = mesh.NumNormals,
                NumTexturedRectangles = 0,
                NumTexturedTriangles = 0,
                NumVertices = mesh.NumVertices,
                Pointer = mesh.Pointer,
                TexturedRectangles = new TRFace4[] { },
                TexturedTriangles = new TRFace3[] { },
                Vertices = new TRVertex[mesh.NumVertices]
            };

            for (int i = 0; i < mesh.NumTexturedRectangles; i++)
            {
                clone.ColouredRectangles[i] = new TRFace4
                {
                    Texture = paletteIndex,
                    Vertices = mesh.TexturedRectangles[i].Vertices
                };
            }

            for (int i = 0; i < mesh.NumTexturedTriangles; i++)
            {
                clone.ColouredTriangles[i] = new TRFace3
                {
                    Texture = paletteIndex,
                    Vertices = mesh.TexturedTriangles[i].Vertices
                };
            }

            for (int i = 0; i < mesh.NumVertices; i++)
            {
                clone.Vertices[i] = new TRVertex
                {
                    X = mesh.Vertices[i].X,
                    Y = mesh.Vertices[i].Y,
                    Z = mesh.Vertices[i].Z
                };
            }

            return clone;
        }

        static void Cutscene1BraidExport()
        {
            // A textured braid plus edits to Lara's head to make it
            // appear more natural.

            TRLevel level = InjectionIO.CreateLevelBase(1);

            new TR1ModelImporter
            {
                Level = level,
                EntitiesToImport = new List<TREntities> { TREntities.LaraPonytail_H_U },
                DataFolder = @"Resources\Models"
            }.Import();

            InjectionData data = InjectionData.CreateFromLevel(level);
            data.InjectionType = InjectionType.Braid;

            TRMeshEdit headEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.CutsceneActor1,
                MeshIndex = 14,
                FaceEdits = new List<TRFaceTextureEdit>
                {
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 0,
                        FaceType = TRFaceType.TexturedQuad,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 6 }
                    },
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 5,
                        FaceType = TRFaceType.TexturedTriangle,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 53, 54, 55, 56, 57, 58, 59, 60 }
                    }
                },
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 45,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 44,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 43,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    }
                }
            };

            data.MeshEdits = new List<TRMeshEdit>
            {
                headEdit
            };

            InjectionIO.Export(data, @"Output\braid_cut1.bin");
        }

        static void Cutscene24BraidExport()
        {
            // A textured braid plus edits to Lara's head to make it
            // appear more natural.

            TRLevel level = InjectionIO.CreateLevelBase(1);

            new TR1ModelImporter
            {
                Level = level,
                EntitiesToImport = new List<TREntities> { TREntities.LaraPonytail_H_U },
                DataFolder = @"Resources\Models"
            }.Import();

            InjectionData data = InjectionData.CreateFromLevel(level);
            data.InjectionType = InjectionType.Braid;

            TRMeshEdit headEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.CutsceneActor1,
                MeshIndex = 14,
                FaceEdits = new List<TRFaceTextureEdit>
                {
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 0,
                        FaceType = TRFaceType.TexturedQuad,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 1 }
                    },
                    new TRFaceTextureEdit
                    {
                        ModelID = (uint)TREntities.LaraPonytail_H_U,
                        MeshIndex = 5,
                        FaceType = TRFaceType.TexturedTriangle,
                        FaceIndex = 0,
                        TargetFaceIndices = new List<short> { 66, 67, 68, 69, 70, 71, 72, 73 }
                    }
                },
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 45,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 44,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 43,
                        Change = new TRVertex
                        {
                            Y = -16
                        }
                    }
                }
            };

            data.MeshEdits = new List<TRMeshEdit>
            {
                headEdit
            };

            InjectionIO.Export(data, @"Output\braid_cut2_cut4.bin");
        }

        static void BackpackExport()
        {
            // Edits to Lara's backpack for use when the braid is
            // imported. Seprated to allow the braid to be used
            // in the gym.

            TRLevel level = InjectionIO.CreateLevelBase(0);

            InjectionData data = InjectionData.CreateFromLevel(level);
            data.InjectionType = InjectionType.Braid;

            TRMeshEdit backpackEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.Lara,
                MeshIndex = 7,
                FaceEdits = new List<TRFaceTextureEdit>(),
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 26,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 27,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 28,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 29,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                }
            };
            TRMeshEdit backpackShotgunEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.LaraShotgunAnim_H,
                MeshIndex = 7,
                FaceEdits = new List<TRFaceTextureEdit>(),
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 26,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 27,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 28,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 29,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                }
            };

            data.MeshEdits = new List<TRMeshEdit>
            {
                backpackEdit, backpackShotgunEdit
            };

            InjectionIO.Export(data, @"Output\backpack.bin");
        }
        static void CutsceneBackpackExport()
        {
            // Edits to Lara's backpack for use when the braid is
            // imported. Seprated to allow the braid to be used
            // in the gym.

            TRLevel level = InjectionIO.CreateLevelBase(0);

            InjectionData data = InjectionData.CreateFromLevel(level);
            data.InjectionType = InjectionType.Braid;

            TRMeshEdit backpackEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.CutsceneActor1,
                MeshIndex = 7,
                FaceEdits = new List<TRFaceTextureEdit>(),
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 26,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 27,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 28,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 29,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                }
            };
            TRMeshEdit backpackShotgunEdit = new TRMeshEdit
            {
                ModelID = (uint)TREntities.LaraShotgunAnim_H,
                MeshIndex = 7,
                FaceEdits = new List<TRFaceTextureEdit>(),
                VertexChanges = new List<TRVertexChange>
                {
                    new TRVertexChange
                    {
                        VertexIndex = 26,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 27,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 28,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                    new TRVertexChange
                    {
                        VertexIndex = 29,
                        Change = new TRVertex
                        {
                            Z = 12
                        }
                    },
                }
            };

            data.MeshEdits = new List<TRMeshEdit>
            {
                backpackEdit, backpackShotgunEdit
            };

            InjectionIO.Export(data, @"Output\backpack_cut.bin");
        }

     */
}
