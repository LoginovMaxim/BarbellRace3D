using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Assemblers
{
    public abstract class Assembler : IAssembler, IDisposable
    {
        private readonly Queue<IAssemblerPart> _assemblerParts = new Queue<IAssemblerPart>();
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();

        private int _servicesCount;
        private int _currentStepCount;
        private float _progress;

        protected Assembler(List<IAssemblerPart> assemblerParts)
        {
#pragma warning disable CS4014
            LaunchAssemblerPartsAsync(assemblerParts);
#pragma warning restore CS4014
        }

        private async Task LaunchAssemblerPartsAsync(List<IAssemblerPart> assemblerParts)
        {
            foreach (var assemblerPart in assemblerParts)
            {
                _assemblerParts.Enqueue(assemblerPart);
            }
            
            _servicesCount = _assemblerParts.Count;
            
            while (_assemblerParts.Count > 0)
            {
                if (_tokenSource.IsCancellationRequested)
                {
                    return;
                }
                
                var assemblerPart = _assemblerParts.Peek();
                try
                {
                    Debug.Log($"Launching service: {assemblerPart.GetType()}");
                    await assemblerPart.Launch();
                }
                catch (Exception e)
                {
                    Debug.Log($"Service {assemblerPart.GetType()} was error launch.".WithColor(LoggerColor.Red) +
                              $"\nMessage {e.Message}." +
                              $"\nStacktrace: {e.StackTrace}.");

                    await Task.Delay(100);
                    continue;
                }
                
                _assemblerParts.Dequeue();
                _currentStepCount++;
                _progress = (float) _servicesCount / _currentStepCount;
                Debug.Log($"Service: {assemblerPart.GetType()} launched successfully".WithColor(LoggerColor.Green));
            }
        }

        private void Dispose()
        {
            _tokenSource.Cancel();
        }

        #region IAssembler

        int IAssembler.ServicesCount => _servicesCount;
        int IAssembler.CurrentStepCount => _currentStepCount;
        float IAssembler.Progress => _progress;
        
        #endregion
        
        #region IDisposable

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion
    }
}