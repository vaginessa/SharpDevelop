// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Martin Kon��ek" email="martin.konicek@gmail.com"/>
//     <version>$Revision$</version>
// </file>
using Debugger.MetaData;
using Debugger.AddIn.Visualizers.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using ICSharpCode.SharpDevelop.Services;
using ICSharpCode.NRefactory.Ast;

namespace Debugger.AddIn.Visualizers.GridVisualizer
{
	/// <summary>
	/// Provides <see cref="ObjectValue"/>s to be displayed in Grid visualizer.
	/// Descandants implement getting values for IList and IEnumerable.
	/// </summary>
	public class GridValuesProvider
	{
		protected readonly Debugger.MetaData.BindingFlags memberBindingFlags =
			BindingFlags.Public | BindingFlags.Instance | BindingFlags.Field | BindingFlags.GetProperty;
		
		/// <summary> Used to quickly find MemberInfo by member name - DebugType.GetMember(name) uses a loop to search members </summary>
		protected Dictionary<string, MemberInfo> memberFromNameMap;
		
		protected Expression targetObject;
		protected DebugType collectionType;
		protected DebugType itemType;
		
		public GridValuesProvider(Expression targetObject, DebugType collectionType, DebugType itemType)
		{
			this.targetObject = targetObject;
			this.collectionType = collectionType;
			this.itemType = itemType;
			
			this.memberFromNameMap = this.GetItemTypeMembers().MakeDictionary(memberInfo => memberInfo.Name);
		}
		
		/// <summary>
		/// Gets members that will be displayed as columns.
		/// </summary>
		public IList<MemberInfo> GetItemTypeMembers()
		{
			return itemType.GetMembers(this.memberBindingFlags);
		}
	}
}
