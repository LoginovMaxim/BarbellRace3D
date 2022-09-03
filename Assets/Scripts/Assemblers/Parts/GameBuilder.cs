﻿using System.Threading.Tasks;
using App.Monos;
using ViewModels;

namespace App.Assemblers.Parts
{
    public sealed class GameBuilder : IAssemblerPart
    {
        private readonly IGameSpawnManager _gameSpawnManager;
        private readonly PlayerViewModel.Factory _playerViewModelFactory;

        private PlayerViewModel _playerViewModel;

        public GameBuilder(
            IGameSpawnManager gameSpawnManager,
            PlayerViewModel.Factory playerViewModelFactory)
        {
            _gameSpawnManager = gameSpawnManager;
            _playerViewModelFactory = playerViewModelFactory;
            _playerViewModel = _playerViewModelFactory.Create(_gameSpawnManager.PlayerSpawnPosition);
        }

        public Task Launch()
        {
            return Task.CompletedTask;
        }
    }
}