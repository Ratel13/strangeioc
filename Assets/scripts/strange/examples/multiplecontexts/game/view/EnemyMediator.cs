/*
 * Copyright 2013 ThirdMotion, Inc.
 *
 *	Licensed under the Apache License, Version 2.0 (the "License");
 *	you may not use this file except in compliance with the License.
 *	You may obtain a copy of the License at
 *
 *		http://www.apache.org/licenses/LICENSE-2.0
 *
 *		Unless required by applicable law or agreed to in writing, software
 *		distributed under the License is distributed on an "AS IS" BASIS,
 *		WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *		See the License for the specific language governing permissions and
 *		limitations under the License.
 */

/// Ship mediator
/// =====================
/// Make your Mediator as thin as possible. Its function is to mediate
/// between view and app. Don't load it up with behavior that belongs in
/// the View (listening to/controlling interface), Commands (business logic),
/// Models (maintaining state) or Services (reaching out for data).

using System;
using UnityEngine;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.mediation.impl;

namespace strange.examples.multiplecontexts.game
{
	public class EnemyMediator : EventMediator
	{
		[Inject]
		public EnemyView view{ get; set;}
		
		public override void onRegister()
		{
			updateListeners(true);
			view.init ();
		}
		
		public override void onRemove()
		{
			updateListeners(false);
		}
		
		private void updateListeners(bool value)
		{
			view.dispatcher.updateListener(value, EnemyView.CLICK_EVENT, onViewClicked);
			dispatcher.updateListener( value, GameEvent.GAME_UPDATE, onGameUpdate);
			dispatcher.updateListener( value, GameEvent.GAME_OVER, onGameOver);
			
			dispatcher.addListener(GameEvent.RESTART_GAME, onRestart);
		}
		
		private void onViewClicked()
		{
			dispatcher.Dispatch(GameEvent.ADD_TO_SCORE, 10);
		}
		
		private void onGameUpdate()
		{
			view.updatePosition();
		}
		
		private void onGameOver()
		{
			updateListeners(false);
		}
		
		private void onRestart()
		{
			onRegister();
		}
	}
}

