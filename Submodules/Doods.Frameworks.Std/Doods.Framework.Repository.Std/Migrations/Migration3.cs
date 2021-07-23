// ---------------------------------------------------------------------------
//  This source file is the confidential property and copyright of WIUZ.
//  Reproduction or transmission in whole or in part, in any form or
//  by any means, electronic, mechanical or otherwise, is prohibited
//  without the prior written consent of the copyright owner.
//  <copyright file="Migration3.cs" company="WIUZ">
//     Copyright (C) WIUZ.  All rights reserved. 2016 - 2021
//  </copyright>
//  History:
//   2021/01/26 at 13:08: Thibault HERVIOU aka ThibaultHERVIOU.
// ---------------------------------------------------------------------------

using Doods.Framework.Repository.Std.Interfaces;
using SQLite;

namespace Doods.Framework.Repository.Std.Migrations
{
    internal class Migration3 : IMigration
    {
        public int VersionNumber => 3;

        public void Run(SQLiteConnection connection)
        {
            connection.CreateCommand("ALTER TABLE Host ADD COLUMN IsWebminServer integer");
        }
    }
}