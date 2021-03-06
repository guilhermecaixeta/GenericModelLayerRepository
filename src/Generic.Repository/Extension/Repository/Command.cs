﻿using Generic.Repository.Extension.List;
using Generic.Repository.Interfaces.Repository;
using Generic.Repository.ThrowError;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Generic.Repository.Extension.Repository
{
    public static class Command
    {
        /// <summary>Bulks the delete asynchronous.</summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="list">The list.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <param name="token">The token.</param>
        public static async Task BulkDeleteAsync<TValue, TContext>(
            this IBaseRepositoryAsync<TValue, TContext> repository,
            IEnumerable<TValue> list,
            int chunkSize,
            CancellationToken token)
            where TValue : class
            where TContext : DbContext
        {
            await ProccessTaskAsync(repository, list, chunkSize, repository.DeleteAsync, nameof(BulkDeleteAsync), token).
                ConfigureAwait(false);
        }

        /// <summary>Bulks the insert asynchronous.</summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="list">The list.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <param name="token">The token.</param>
        public static async Task BulkInsertAsync<TValue, TContext>(
            this IBaseRepositoryAsync<TValue, TContext> repository,
            IEnumerable<TValue> list,
            int chunkSize,
            CancellationToken token)
            where TValue : class
            where TContext : DbContext
        {
            await ProccessTaskAsync(repository, list, chunkSize, repository.CreateAsync, nameof(BulkInsertAsync), token).
                ConfigureAwait(false);
        }

        /// <summary>Bulks the update asynchronous.</summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="list">The list.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <param name="token">The token.</param>
        public static async Task BulkUpdateAsync<TValue, TContext>(
            this IBaseRepositoryAsync<TValue, TContext> repository,
            IEnumerable<TValue> list,
            int chunkSize,
            CancellationToken token)
            where TValue : class
            where TContext : DbContext
        {
            await ProccessTaskAsync(repository, list, chunkSize, repository.UpdateAsync, nameof(BulkUpdateAsync), token).
                ConfigureAwait(false);
        }

        /// <summary>
        /// Proccess the task asynchronously.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <param name="list">The list.</param>
        /// <param name="chunkSize">Size of the chunk.</param>
        /// <param name="task">The task.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="token">The token.</param>
        internal static async Task ProccessTaskAsync<TValue, TContext>(
            IBaseRepositoryAsync<TValue, TContext> repository,
            IEnumerable<TValue> list,
            int chunkSize,
            Func<IEnumerable<TValue>, CancellationToken, Task> task,
            string className,
            CancellationToken token)
            where TValue : class
            where TContext : DbContext
        {
            ThrowErrorIf.IsNullValue(repository, nameof(repository), className);

            ThrowErrorIf.IsNullOrEmptyList(list, nameof(list), className);

            ThrowErrorIf.IsLessThanOrEqualsZero(chunkSize);

            await repository.MultiTransactionsAsync(
                async ctx =>
                {
                    var listSplit = list.SplitList(chunkSize);

                    foreach (var split in listSplit)
                    {
                        await task(split, token).
                            ConfigureAwait(false);
                    }
                }, token).ConfigureAwait(false);
        }
    }
}